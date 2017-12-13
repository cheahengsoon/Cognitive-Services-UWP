using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace CogsExplorer.Modules.Translation.Helpers
{
    public static class SpeechHelper
    {
        public async static Task SpeakTranslationAsync(IInputStream file)
        {
            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();

            await RandomAccessStream.CopyAsync(file, stream);
            stream.Seek(0);

            MediaElement element = new MediaElement();
            element.SetSource(stream, "audio/x-wav");
            element.Play();

            return;
        }
    }
}
