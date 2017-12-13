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

namespace CogsExplorer.Modules.LUIS
{
    public class ServiceViewModel : ObservableBase
    {
        public ServiceViewModel()
        {
            SendQuestionCommand = new RelayCommand(async () => { await SendQuestionCommandAsync(); });
        }

        public ICommand SendQuestionCommand { get; private set; }

        private ConversationInformation _conversation = new ConversationInformation();
        public ConversationInformation Conversation
        {
            get { return _conversation; }
            set { Set(ref _conversation, value); }
        }

        private string _currentQuestion;
        public string CurrentQuestion
        {
            get { return _currentQuestion; }
            set { Set(ref _currentQuestion, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        public async void Initialize()
        {
           await SendQuestionCommandAsync();
        }

        public async Task GetIntentionCommandAsync()
        {
            //string question = (this.CurrentQuestion + "").Trim();

            //if (!string.IsNullOrEmpty(question))
            //{
            //    AddQuestion(question);
            //}

            //var result = await Helpers.AnswerHelper.GetIntentionAsync(question);

            //var intention = result.intents.FirstOrDefault();
            //var entity = result.entities.FirstOrDefault();
            
            return;
        }

        //public async Task SendQuestionCommandAsync()
        //{
        //    string question = (this.CurrentQuestion + "").Trim();

        //    if (!string.IsNullOrEmpty(question))
        //    {
        //        AddQuestion(question);
        //    }
        //    else
        //    {
        //        question = "Hi";
        //    }

        //    var answer = await Helpers.AnswerHelper.AskAsync($"{question}");

        //    AddAnswer(answer);

        //    return;
        //}

        public async Task SendQuestionCommandAsync()
        {
            string question = (this.CurrentQuestion + "").Trim();

            if (!string.IsNullOrEmpty(question))
            {
                AddQuestion(question);
            }
            else
            {
                question = "Hi";
            }

            var answer = await Helpers.AnswerHelper.AskAsync($"{question}");

            if (answer.Equals("No good match found in the KB") || answer.Equals("I actually don't know the answer to this..."))
            {
                var result = await Helpers.AnswerHelper.GetIntentionAsync(question);

                var intention = result.intents.FirstOrDefault().intent;
                var entity = result.entities.FirstOrDefault().entity;

                answer = await Helpers.AnswerHelper.AskAsync($"{intention} {entity}");
            }

            AddAnswer(answer);

            return;
        }

        public void AddQuestion(string question)
        {
            this.IsBusy = true;

            this.Conversation.Add(new ConversationItem()
            {
                Content = question,
                Timestamp = DateTime.Now,
                IsBot = false,
            });

            this.CurrentQuestion = "";
        }

        public void AddAnswer(string answer)
        {
            this.IsBusy = false;

            this.Conversation.Add(new ConversationItem()
            {
                Content = answer,
                Timestamp = DateTime.Now,
                IsBot = true,
            });
            
        }

       
        
    }
}
