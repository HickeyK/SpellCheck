using SpellCheck.Entities;
using System;
using System.ComponentModel;

namespace SpellCheck.ViewModel
{
    public class SpellingViewModel : INotifyPropertyChanged
    {

        private readonly Spelling _item;
        private ushort _errorCount;
        private bool _skipped;
        private ushort _correctCount;
        private string _finalAnswer;
        private QuestionResult _answerStatus;

        //private QuestionResult Result { get; set; }

        public SpellingViewModel(Spelling spelling)
        {
            _item = spelling;
        }

        public Spelling GetItem()
        {
            return _item;
        }

        public int Id
        {
            get { return _item.Id; }
        }


        public string Word
        {
            get { return _item.Word; }
            //set { _item.Word = value; }
        }


        public string ContextSentence
        {
            get { return _item.ContextSentence; }
            //set { _item.ContextSentence = value; }
        }


        public UInt16 CorrectCount
        {
            get { return _correctCount; }
            set
            {
                if (_correctCount != value)
                {
                    _correctCount = value;
                    OnPropertyChanged("CorrectCount");
                }
            }
        }
        public bool Skipped
        {
            get { return _skipped; }
            set
            {
                if (_skipped != value)
                {
                    _skipped = value;
                    OnPropertyChanged("Skipped");
                }
            }
        }

        public UInt16 ErrorCount
        {
            get { return _errorCount; }
            set
            {
                if (_errorCount != value)
                {
                    _errorCount = value;
                    OnPropertyChanged("ErrorCount");
                }
            }
        }

        public string FinalAnswer
        {
            get { return _finalAnswer; }
            set
            {
                if (_finalAnswer != value)
                {
                    _finalAnswer = value;
                    OnPropertyChanged("FinalAnswer");
                }
            }
        }


        public QuestionResult AnswerStatus
        {
            get { return _answerStatus; }
            set
            {
                if (_answerStatus != value)
                {
                    _answerStatus = value;
                    OnPropertyChanged("AnswerStatus");
                }
            }
        }


        #region INPC

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
