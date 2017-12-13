using CogsExplorer.Common;
using CogsExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace CogsExplorer.Modules.Translation
{
    public class ServiceViewModel : ObservableBase
    {
        public ServiceViewModel()
        {
            InitializeLanguages();

            TranslateTextCommand = new RelayCommand(async () => { await StartTranslateTextAsync(); });
            ClearContentCommand = new RelayCommand(ClearContent);

            this.SpeechClient = new SpeechTranslationInformation();
            this.SpeechHelper = new Helpers.SpeechTranslationHelper();
            this.SpeechListener = new Helpers.SpeechListener();

            StartSpeechListenerCommand = new RelayCommand(StartSpeechListener);
            StopSpeechListenerCommand = new RelayCommand(StopSpeechListener);

            InitializeSpeechLanguages();
        }

        private void StartSpeechListener()
        {
            this.IsListeningForSpeech = true;

            this.SpeechListener.StartListening(this);
        }

        private void StopSpeechListener()
        {
            this.SpeechListener.StopListening();

            this.IsListeningForSpeech = false;
        }

        public ICommand TranslateTextCommand { get; private set; }
        public ICommand ClearContentCommand { get; private set; }

        public ICommand StartSpeechListenerCommand { get; private set; }
        public ICommand StopSpeechListenerCommand { get; private set; }

        public SpeechTranslationInformation SpeechClient { get; set; }
        public Helpers.SpeechTranslationHelper SpeechHelper { get; set; }
        public Helpers.SpeechListener SpeechListener { get; set; }

        private ObservableCollection<LanguageInformation> _availableLanguages = new ObservableCollection<LanguageInformation>();
        public ObservableCollection<LanguageInformation> AvailableLanguages
        {
            get { return _availableLanguages; }
            set { Set(ref _availableLanguages, value); }
        }
        
        private LanguageInformation _selectedLanguage;

        public async void InitializeSpeechServicesAsync()
        {
            foreach (var device in await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Media.Devices.MediaDevice.GetAudioCaptureSelector()))
            {
                this.AvailableMicrophones.Add(device);
            }

            this.SelectedMicrophone = this.AvailableMicrophones.FirstOrDefault();

            foreach (var device in await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Media.Devices.MediaDevice.GetAudioRenderSelector()))
            {
                this.AvailableSpeakers.Add(device);
            }

            this.SelectedSpeaker = this.AvailableSpeakers.FirstOrDefault();
        }

        public LanguageInformation SelectedLanguage
        {
            get { return _selectedLanguage; }
            set { Set(ref _selectedLanguage, value); }
        }

        private string _currentTextContent;
        public string CurrentTextContent
        {
            get { return _currentTextContent; }
            set { Set(ref _currentTextContent, value); }
        }

        private ObservableCollection<TranslationInformation> _translations = new ObservableCollection<TranslationInformation>();
        public ObservableCollection<TranslationInformation> Translations
        {
            get { return _translations; }
            set { Set(ref _translations, value); }
        }

        private TranslationInformation _selectedTranslation;
        public TranslationInformation SelectedTranslation
        {
            get { return _selectedTranslation; }
            set { Set(ref _selectedTranslation, value); }
        }

        private ObservableCollection<Windows.Devices.Enumeration.DeviceInformation> _availableMicrophones = new ObservableCollection<Windows.Devices.Enumeration.DeviceInformation>();
        public ObservableCollection<Windows.Devices.Enumeration.DeviceInformation> AvailableMicrophones
        {
            get { return _availableMicrophones; }
            set { Set(ref _availableMicrophones, value); }
        }

        private ObservableCollection<Windows.Devices.Enumeration.DeviceInformation> _availableSpeakers = new ObservableCollection<Windows.Devices.Enumeration.DeviceInformation>();
        public ObservableCollection<Windows.Devices.Enumeration.DeviceInformation> AvailableSpeakers
        {
            get { return _availableSpeakers; }
            set { Set(ref _availableSpeakers, value); }
        }

        private Windows.Devices.Enumeration.DeviceInformation _selectedMicrophone;
        public Windows.Devices.Enumeration.DeviceInformation SelectedMicrophone
        {
            get { return _selectedMicrophone; }
            set { Set(ref _selectedMicrophone, value); }
        }

        private Windows.Devices.Enumeration.DeviceInformation _selectedSpeaker;
        public Windows.Devices.Enumeration.DeviceInformation SelectedSpeaker
        {
            get { return _selectedSpeaker; }
            set { Set(ref _selectedSpeaker, value); }
        }

        private SpeechLanguageCollection _availableSpeechLanguages = new SpeechLanguageCollection();
        public SpeechLanguageCollection AvailableSpeechLanguages
        {
            get { return _availableSpeechLanguages; }
            set { Set(ref _availableSpeechLanguages, value); }
        }

        private SpeechLanguageInformation _selectedTextLanguage;
        public SpeechLanguageInformation SelectedTextLanguage
        {
            get { return _selectedTextLanguage; }
            set {

                Set(ref _selectedTextLanguage, value);
                this.SelectedSpeechVoice = value.Voices.FirstOrDefault();
            }
        }

        private SpeechLanguageInformation _selectedSpeechLanguage;
        public SpeechLanguageInformation SelectedSpeechLanguage
        {
            get { return _selectedSpeechLanguage; }
            set
            {
                Set(ref _selectedSpeechLanguage, value);

               
            }
        }

        private SpeechVoiceInformation _selectedSpeechVoice;
        public SpeechVoiceInformation SelectedSpeechVoice
        {
            get { return _selectedSpeechVoice; }
            set { Set(ref _selectedSpeechVoice, value); }
        }

        private bool _isListeningForSpeech;
        public bool IsListeningForSpeech
        {
            get { return _isListeningForSpeech; }
            set { Set(ref _isListeningForSpeech, value); }
        }
        
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        public async void InitializeLanguages()
        {
            this.IsBusy = true;

            var availableLanguages = await Helpers.TranslationHelper.GetTextTranslationLanguageNamesAsync();

            foreach(var language in availableLanguages)
            {
                this.AvailableLanguages.Add(language);
            }

            this.SelectedLanguage = this.AvailableLanguages.FirstOrDefault();

            this.IsBusy = false;
        }

        public async void InitializeSpeechLanguages()
        {
            this.IsBusy = true;

            this.AvailableSpeechLanguages = await Helpers.TranslationHelper.GetSpeechTranslationLanguagesAsync();

            this.SelectedSpeechLanguage = this.AvailableSpeechLanguages.SpeechLanguages.Where(w => w.Abbreviation.Equals("en-US")).FirstOrDefault();
            this.SelectedTextLanguage = this.AvailableSpeechLanguages.TextLanguages.Where(w => w.Abbreviation.Equals("es")).FirstOrDefault();

            this.IsBusy = false;
        }

        private void ClearContent()
        {
            this.Translations.Clear();
            this.CurrentTextContent = "";
            this.SelectedLanguage = this.AvailableLanguages.FirstOrDefault();
        }

        public async Task StartTranslateTextAsync()
        {
            this.IsBusy = true;
            
            string translatedText = await Helpers.TranslationHelper.GetTextTranslationAsync(this.CurrentTextContent, this.SelectedLanguage.Abbreviation);

            this.Translations.Add(new TranslationInformation()
            {
                OriginalContent = this.CurrentTextContent,
                TranslatedContent = translatedText,
                TranslatedLanguage = new LanguageInformation()
                {
                    Abbreviation = this.SelectedLanguage.Abbreviation,
                    DisplayName = this.SelectedLanguage.DisplayName,
                    SupportsSpeech = this.SelectedLanguage.SupportsSpeech,
                }
            });

            this.IsBusy = false;

            return;
        }

    }
}
