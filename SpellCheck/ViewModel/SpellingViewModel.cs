using SpellCheck.Entities;
using System;
using System.ComponentModel;

namespace SpellCheck.ViewModel
{
    public class SpellingViewModel : INotifyPropertyChanged
    {

        private readonly Spelling _item;

        public SpellingViewModel(Spelling spelling)
        {
            _item = spelling;
        }


        public string Word
        {
            get { return _item.Word; }
            set { _item.Word = value; }
        }


        public string ContextSentence
        {
            get { return _item.ContextSentence; }
            set { _item.ContextSentence = value; }
        }


        public UInt16 CorrectCount
        {
            get { return _item.CorrectCount; }
            set
            {
                if (_item.CorrectCount != value)
                {
                    _item.CorrectCount = value;
                    OnPropertyChanged("CorrectCount");
                }
            }
        }
        public bool Skipped
        {
            get { return _item.Skipped; }
            set
            {
                if (_item.Skipped != value)
                {
                    _item.Skipped = value;
                    OnPropertyChanged("Skipped");
                }
            }
        }

        public UInt16 ErrorCount
        {
            get { return _item.ErrorCount; }
            set
            {
                if (_item.ErrorCount != value)
                {
                    _item.ErrorCount = value;
                    OnPropertyChanged("ErrorCount");
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
