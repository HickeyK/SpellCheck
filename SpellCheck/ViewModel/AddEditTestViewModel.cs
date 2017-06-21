﻿using SpellCheck.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.ViewModel
{
    class AddEditTestViewModel : BindableBase
    {
        #region Fields

        private ConnectedRepository _repo;

        #endregion


        #region Constructors

        public AddEditTestViewModel(ConnectedRepository repo)
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            _repo = repo;

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }


        #endregion

        #region Events

        public event Action Done = delegate { };

        #endregion


        #region Properties

        public RelayCommand SaveCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        private bool _editMode;
        public bool EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }


        private SpellTest _currentTest;
        public SpellTest CurrentTest
        {
            get { return _currentTest; }
            set { SetProperty(ref _currentTest, value); }
        }


        private ObservableCollection<SpellingViewModel> _spellings;
        public ObservableCollection<SpellingViewModel> Spellings
        {
            get { return _spellings; }
            set { SetProperty(ref _spellings, value); }
        }

        #endregion


        #region Methods
        #endregion



        #region EventHandlers

        protected void OnSave()
        {
            CurrentTest.Spellings = new List<Spelling>();
            foreach (var s in Spellings)
            {
                CurrentTest.Spellings.Add(s.GetItem());
            }

            if (EditMode)
            {
                _repo.UpdateTest(CurrentTest); 
            }
            else
            {
                _repo.AddTest(CurrentTest);
            }
            Done();
        }

        protected void OnCancel()
        {
            Done();
        }


        #endregion
    }

}
