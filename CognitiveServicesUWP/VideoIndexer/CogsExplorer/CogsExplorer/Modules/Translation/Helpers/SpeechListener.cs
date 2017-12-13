using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Audio;

namespace CogsExplorer.Modules.Translation.Helpers
{
    public class SpeechListener
    {
        AudioGraph graph;
        AudioFrameOutputNode speechTranslateOutputMode;
        AudioDeviceOutputNode speakerOutputNode;
        AudioFrameInputNode textToSpeechOutputNode;

        ServiceViewModel viewModel;

        public async void StartListening(ServiceViewModel model)
        {
            this.viewModel = model;

            await this.viewModel.SpeechClient.Clear();

            var fromValue = this.viewModel.SelectedSpeechLanguage.Abbreviation;
            var toValue = this.viewModel.SelectedTextLanguage.Abbreviation;
            var voiceValue = this.viewModel.SelectedSpeechVoice.Name;

            await this.viewModel.SpeechHelper.Connect(fromValue, toValue, voiceValue, this.DisplayResult, this.SendAudioOut);

            var pcmEncoding = Windows.Media.MediaProperties.AudioEncodingProperties.CreatePcm(16000, 1, 16);

            var result = await Windows.Media.Audio.AudioGraph.CreateAsync(
              new Windows.Media.Audio.AudioGraphSettings(Windows.Media.Render.AudioRenderCategory.Speech)
              {
                  DesiredRenderDeviceAudioProcessing = Windows.Media.AudioProcessing.Raw,
                  AudioRenderCategory = Windows.Media.Render.AudioRenderCategory.Speech,
                  EncodingProperties = pcmEncoding
              });

            if (result.Status == Windows.Media.Audio.AudioGraphCreationStatus.Success)
            {
                this.graph = result.Graph;

                var microphone = await DeviceInformation.CreateFromIdAsync(this.viewModel.SelectedMicrophone.Id);

                this.speechTranslateOutputMode = this.graph.CreateFrameOutputNode(pcmEncoding);
                this.graph.QuantumProcessed += (s, a) => this.SendToSpeechTranslate(this.speechTranslateOutputMode.GetFrame());

                this.speechTranslateOutputMode.Start();

                var micInputResult = await this.graph.CreateDeviceInputNodeAsync(Windows.Media.Capture.MediaCategory.Speech, pcmEncoding, microphone);

                if (micInputResult.Status == Windows.Media.Audio.AudioDeviceNodeCreationStatus.Success)
                {
                    micInputResult.DeviceInputNode.AddOutgoingConnection(this.speechTranslateOutputMode);
                    micInputResult.DeviceInputNode.Start();
                }
                else
                {
                    throw new InvalidOperationException();
                }

                var speakerOutputResult = await this.graph.CreateDeviceOutputNodeAsync();

                if (speakerOutputResult.Status == Windows.Media.Audio.AudioDeviceNodeCreationStatus.Success)
                {
                    this.speakerOutputNode = speakerOutputResult.DeviceOutputNode;
                    this.speakerOutputNode.Start();
                }
                else
                {
                    throw new InvalidOperationException();
                }

                this.textToSpeechOutputNode = this.graph.CreateFrameInputNode(pcmEncoding);
                this.textToSpeechOutputNode.AddOutgoingConnection(this.speakerOutputNode);
                this.textToSpeechOutputNode.Start();

                this.graph.Start();
            }
        }

        private async void DisplayResult(SpeechTranslationResult result)
        {
            await this.viewModel.SpeechClient.Add(result);
        }

        private void SendAudioOut(Windows.Media.AudioFrame frame)
        {
            this.textToSpeechOutputNode.AddFrame(frame);
        }

        private void SendToSpeechTranslate(Windows.Media.AudioFrame frame)
        {
            this.viewModel.SpeechHelper.SendAudioFrame(frame);
        }

        public void StopListening()
        {
            this.viewModel.SpeechHelper.Close();

            if (this.graph != null)
            {
                this.graph?.Stop();
                this.graph?.Dispose();
                this.graph = null;
            }
        }

    }
}
