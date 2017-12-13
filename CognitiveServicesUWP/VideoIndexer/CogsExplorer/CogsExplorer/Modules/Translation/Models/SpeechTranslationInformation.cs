using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace CogsExplorer.Modules.Translation
{
    public class SpeechTranslationResult
    {

        /// <summary>
        /// Recognition Result
        /// </summary>
        public string Recognition { get; set; }

        /// <summary>
        /// Translation Result
        /// </summary>
        public string Translation { get; set; }

        /// <summary>
        /// Status Message to be displayed in case of error
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// ViewModel that connects to the ListView
    /// </summary>
    public class SpeechTranslationInformation
    {
        private ObservableCollection<SpeechTranslationResult> results = new ObservableCollection<SpeechTranslationResult>();
        private CoreDispatcher dispatcher;

        /// <summary>
        /// List of Results that will populate the UI List View
        /// </summary>
        public ObservableCollection<SpeechTranslationResult> Results { get { return results; } }

        public SpeechTranslationInformation()
        {
            dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
        }

        /// <summary>
        /// Clear the result list
        /// </summary>
        /// <returns></returns>
        public async Task Clear()
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => results.Clear());
        }

        /// <summary>
        /// Add to the result list
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task Add(SpeechTranslationResult result)
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => results.Add(result));
        }
    }
}
