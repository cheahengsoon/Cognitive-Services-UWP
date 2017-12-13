using CogsExplorer.Common;
using CogsExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CogsExplorer.Modules.Translation
{
    public class TranslationInformation : ObservableBase
    {
        public TranslationInformation()
        {
            TranslateTextToSpeechCommand = new RelayCommand(async () => { await StartTranslateTextToSpeechAsync(); });
        }

        public ICommand TranslateTextToSpeechCommand { get; private set; }

        private string _originalContent;
        public string OriginalContent
        {
            get { return _originalContent; }
            set { Set(ref _originalContent, value); }
        }

        private string _translatedContent;
        public string TranslatedContent
        {
            get { return _translatedContent; }
            set { Set(ref _translatedContent, value); }
        }

        private LanguageInformation _translatedLanguage;
        public LanguageInformation TranslatedLanguage
        {
            get { return _translatedLanguage; }
            set { Set(ref _translatedLanguage, value); }
        }


        public async Task StartTranslateTextToSpeechAsync()
        {
            //this.IsBusy = true;

            var speechStream = await Helpers.TranslationHelper.GetTextTranslationForSpeechAsync(this.TranslatedContent);

            await Helpers.SpeechHelper.SpeakTranslationAsync(speechStream);

            //this.IsBusy = false;

            return;
        }
    }
}
