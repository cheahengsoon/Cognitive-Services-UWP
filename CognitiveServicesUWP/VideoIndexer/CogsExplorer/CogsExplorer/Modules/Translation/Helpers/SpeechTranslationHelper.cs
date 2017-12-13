
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Core;


namespace CogsExplorer.Modules.Translation.Helpers
{
    public class SpeechTranslationHelper
    {
        public delegate void OnTextToSpeechData(AudioFrame frame);
        public delegate void OnSpeechTranslateResult(SpeechTranslationResult result);

        const string SpeechTranslateUrl = @"wss://dev.microsofttranslator.com/speech/translate?from={0}&to={1}{2}&api-version=1.0";
        private static readonly Encoding UTF8 = new UTF8Encoding();

        private MessageWebSocket webSocket;
        private DataWriter dataWriter;
        private Timer timer;
        private OnTextToSpeechData onTextToSpeechData;
        private OnSpeechTranslateResult onSpeechTranslateResult;

        public async Task Connect(string from, string to, string voice, OnSpeechTranslateResult onSpeechTranslateResult, OnTextToSpeechData onTextToSpeechData)
        {
            this.webSocket = new MessageWebSocket();
            this.onTextToSpeechData = onTextToSpeechData;
            this.onSpeechTranslateResult = onSpeechTranslateResult;

            this.webSocket.SetRequestHeader("Ocp-Apim-Subscription-Key", $"{Common.CoreConstants.TranslatorSpeechSubscriptionKey}");

            var url = String.Format(SpeechTranslateUrl, from, to, voice == null ? "" : "&features=texttospeech&voice=" + voice);

            this.webSocket.MessageReceived += OnMessageReceived;

            this.dataWriter = new DataWriter(this.webSocket.OutputStream);
            this.dataWriter.ByteOrder = ByteOrder.LittleEndian;
            this.dataWriter.WriteBytes(GetWaveHeader());

            await this.webSocket.ConnectAsync(new Uri(url));

            this.timer = new Timer(async (s) =>
            {
                if (this.dataWriter.UnstoredBufferLength > 0)
                {
                    try
                    {
                        await this.dataWriter.StoreAsync();
                    }
                    catch (Exception e)
                    {
                        this.onSpeechTranslateResult(new SpeechTranslationResult() { Status = "DataWriter Failed: " + e.Message });
                    }
                }

                this.timer.Change(TimeSpan.FromMilliseconds(250), Timeout.InfiniteTimeSpan);
            },
            null, TimeSpan.FromMilliseconds(250), Timeout.InfiniteTimeSpan);
        }

        private void OnMessageReceived(MessageWebSocket sender, MessageWebSocketMessageReceivedEventArgs args)
        {
            try
            {
                if (args.MessageType == SocketMessageType.Utf8)
                {
                    string jsonOutput;
                    using (var dataReader = args.GetDataReader())
                    {
                        dataReader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                        jsonOutput = dataReader.ReadString(dataReader.UnconsumedBufferLength);
                    }

                    var result = JsonConvert.DeserializeObject<SpeechTranslationResult>(jsonOutput);
                    this.onSpeechTranslateResult(result);
                }
                else if (args.MessageType == SocketMessageType.Binary)
                {
                    using (var dataReader = args.GetDataReader())
                    {
                        dataReader.ByteOrder = ByteOrder.LittleEndian;
                        this.onTextToSpeechData(AudioFrameHelper.GetAudioFrame(dataReader));
                    }
                }
            }
            catch (Exception e)
            {
                this.onSpeechTranslateResult(new SpeechTranslationResult() { Status = e.Message });
            }
        }

        public void SendAudioFrame(AudioFrame frame)
        {
            AudioFrameHelper.SendAudioFrame(frame, this.dataWriter);
        }

        public void Close()
        {
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);
            this.webSocket.Close((ushort)1000, "end of Stream");
        }

        private byte[] GetWaveHeader()
        {
            var channels = (short)1;
            var sampleRate = 16000;
            var bitsPerSample = (short)16;
            var extraSize = 0;
            var blockAlign = (short)(channels * (bitsPerSample / 8));
            var averageBytesPerSecond = sampleRate * blockAlign;

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
                writer.Write(Encoding.UTF8.GetBytes("RIFF"));
                writer.Write(0);
                writer.Write(Encoding.UTF8.GetBytes("WAVE"));
                writer.Write(Encoding.UTF8.GetBytes("fmt "));
                writer.Write((int)(18 + extraSize));
                writer.Write((short)1);// PCM
                writer.Write((short)channels);
                writer.Write((int)sampleRate);
                writer.Write((int)averageBytesPerSecond);
                writer.Write((short)blockAlign);
                writer.Write((short)bitsPerSample);
                writer.Write((short)extraSize);

                writer.Write(Encoding.UTF8.GetBytes("data"));
                writer.Write(0);

                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public void Dispose()
        {
            if (this.webSocket != null)
            {
                this.webSocket.Dispose();
                this.webSocket = null;
            }
        }
    }

    [ComImport]
    [Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    unsafe interface IMemoryBufferByteAccess
    {
        void GetBuffer(out byte* buffer, out uint capacity);
    }

    internal static class AudioFrameHelper
    {
        internal static void SendAudioFrame(AudioFrame frame, DataWriter writer)
        {
            var audioBuffer = frame.LockBuffer(AudioBufferAccessMode.Read);
            var buffer = Windows.Storage.Streams.Buffer.CreateCopyFromMemoryBuffer(audioBuffer);
            buffer.Length = audioBuffer.Length;
            using (var dataReader = DataReader.FromBuffer(buffer))
            {
                dataReader.ByteOrder = ByteOrder.LittleEndian;
                while (dataReader.UnconsumedBufferLength > 0)
                {
                    writer.WriteInt16(FloatToInt16(dataReader.ReadSingle()));
                }
            }
        }

        unsafe internal static void SendAudioFrameNative(AudioFrame frame, DataWriter writer)
        {
            using (var buffer = frame.LockBuffer(AudioBufferAccessMode.Read))
            using (IMemoryBufferReference reference = buffer.CreateReference())
            {
                byte* dataInBytes;
                uint capacityInBytes;

                ((IMemoryBufferByteAccess)reference).GetBuffer(out dataInBytes, out capacityInBytes);

                float* dataInFloat = (float*)dataInBytes;

                for (int i = 0; i < capacityInBytes / sizeof(float); i++)
                {
                    writer.WriteInt16(FloatToInt16(dataInFloat[i]));
                }
            }
        }

        private static Int16 FloatToInt16(float value)
        {
            float f = value * Int16.MaxValue;
            if (f > Int16.MaxValue) f = Int16.MaxValue;
            if (f < Int16.MinValue) f = Int16.MinValue;
            return (Int16)f;
        }

        unsafe internal static AudioFrame GetAudioFrame(DataReader reader)
        {
            var numBytes = reader.UnconsumedBufferLength;

            var headerSize = 44;
            var bytes = new byte[headerSize];
            reader.ReadBytes(bytes);

            var numSamples = (uint)(numBytes - headerSize);
            AudioFrame frame = new AudioFrame(numSamples);

            using (var buffer = frame.LockBuffer(AudioBufferAccessMode.Write))
            using (IMemoryBufferReference reference = buffer.CreateReference())
            {
                byte* dataInBytes;
                uint capacityInBytes;

                ((IMemoryBufferByteAccess)reference).GetBuffer(out dataInBytes, out capacityInBytes);

                Int16* dataInInt16 = (Int16*)dataInBytes;

                for (int i = 0; i < capacityInBytes / sizeof(Int16); i++)
                {
                    dataInInt16[i] = reader.ReadInt16();
                }
            }

            return frame;
        }
    }
}
