﻿using SpellCheck.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace SpellCheck.ViewModel
{
    public class AnswerViewModel : BindableBase
    {

        #region Fields

        private ISpellTestService _spellTestService;
        private ISpeachService _speachService;

        #endregion

        #region Constructors

        #endregion

        public AnswerViewModel(ObservableCollection<SpellingViewModel> spellTests,
                                     ISpeachService speachService)
        {

            _spellTestService = new SpellTestService(spellTests, speachService);
            _speachService = speachService;

            CurrentSpelling = _spellTestService.NextQuestion();

            RequestSpelling(CurrentSpelling);


            AnswerCommand = new RelayCommand<Window>(OnAnswer, CanAnswer);
            SkipCommand = new RelayCommand<Window>(OnSkip, CanSkip);
            QuitCommand = new RelayCommand<Window>(OnQuit);
            RepeatCommand = new RelayCommand(OnRepeat, CanRepeat);

        }

        #region Events

        public event Action Done = delegate { };

        #endregion

        #region Properties

        public RelayCommand<Window> AnswerCommand { get; set; }
        public RelayCommand<Window> SkipCommand { get; set; }
        public RelayCommand<Window> QuitCommand { get; set; }
        public RelayCommand RepeatCommand { get; set; }


        private SpellingViewModel _currentSpelling;
        public SpellingViewModel CurrentSpelling
        {
            get { return _currentSpelling; }
            set { SetProperty(ref _currentSpelling, value); }
        }


        private string _currentAnswer;
        public string CurrentAnswer
        {
            get { return _currentAnswer; }
            set { SetProperty(ref _currentAnswer, value); }
        }


        #endregion


        #region EventHandlers


        private void OnAnswer(Window window)
        {
            var next = _spellTestService.AnswerQuestion(CurrentAnswer);
            CurrentSpelling = next;
            CurrentAnswer = "";

            if (next == null)
            {
                // All questions answered
                _speachService.Say("Test Completed", true);
                OnQuit(window);
                return;

                // Appears that setting the property to null was not triggering an evaluation of CanExecuteChanged post command execution.
                // It was being called just before command invocation but the command is already triggered at that point so what's the point.
                //AnswerCommand.RaiseCanExecuteChanged();
                //SkipCommand.RaiseCanExecuteChanged();

            }

            RequestSpelling(CurrentSpelling);

        }

        private bool CanAnswer(Window window)
        {
            return CurrentSpelling != null;
        }

        private void OnSkip(Window window)
        {
            var next = _spellTestService.SkipQuestion();
            CurrentSpelling = next;
            CurrentAnswer = "";

            if (next == null)
            {
                // All questions answered

                OnQuit(window);

                // Appears that setting the property to null was not triggering an evaluation of CanExecuteChanged post command execution.
                // It was being called just before command invocation but the command is already triggered at that point so what's the point.

                //AnswerCommand.RaiseCanExecuteChanged();
                //SkipCommand.RaiseCanExecuteChanged();

            }

        }

        private bool CanSkip(Window window)
        {
            return CurrentSpelling != null;
        }



        private void OnQuit(Window window)
        {
            Done();
        }


        private void OnRepeat()
        {
            RequestSpelling(CurrentSpelling);
        }

        private bool CanRepeat()
        {
            return CurrentSpelling != null;
        }


        private void RequestSpelling(SpellingViewModel spellingViewModel)
        {
            var request = spellingViewModel.Word + "," +
                "As in," +
                spellingViewModel.ContextSentence;

            _speachService.Say(request, true);

        }

        #endregion


    }
}