using Moq;
using SpellCheck.Services;
using SpellCheck.Entities;
using SpellCheck.ViewModel;
using System.Collections.Generic;
using Xunit;

namespace Services.Tests
{
    public class SingeQuestionServiceTests
    {



        [Fact]
        public void WhenIsAnsweredCorrectlyAnswerCountIncreases()
        {
            // Arrange
            Mock<ISpeachService> mockSpeachService = new Mock<ISpeachService>();
            List<SpellingViewModel> spellings = new List<SpellingViewModel>();
            Spelling s = new Spelling() { Word = "Dog", ContextSentence = "The dog and bone" };
            var svm = new SpellingViewModel(s);
            spellings.Add(svm);

            var sut = new SpellTestService(spellings, mockSpeachService.Object);
            sut.NextQuestion();

            // Act
            sut.AnswerQuestion("Dog");


            // Assert
            Assert.Equal(svm.CorrectCount, 1);
            Assert.Equal(svm.ErrorCount, 0);
            Assert.Equal(svm.Skipped, false);

        }


        [Fact]
        public void WhenIsAnsweredIncorrectlyErrorCountIncreases()
        {
            // Arrange
            Mock<ISpeachService> mockSpeachService = new Mock<ISpeachService>();
            List<SpellingViewModel> spellings = new List<SpellingViewModel>();
            Spelling s = new Spelling() { Word = "Dog", ContextSentence = "The dog and bone" };
            var svm = new SpellingViewModel(s);
            spellings.Add(svm);

            var sut = new SpellTestService(spellings, mockSpeachService.Object);
            sut.NextQuestion();

            // Act
            sut.AnswerQuestion("Dogg");


            // Assert
            Assert.Equal(svm.CorrectCount, 0);
            Assert.Equal(svm.ErrorCount, 1);
            Assert.Equal(svm.Skipped, false);

        }



        [Fact]
        public void WhenIsSkippedFlagAsSkipped()
        {
            // Arrange
            Mock<ISpeachService> mockSpeachService = new Mock<ISpeachService>();
            List<SpellingViewModel> spellings = new List<SpellingViewModel>();
            Spelling s = new Spelling() { Word = "Dog", ContextSentence = "The dog and bone" };
            var svm = new SpellingViewModel(s);
            spellings.Add(svm);

            var sut = new SpellTestService(spellings, mockSpeachService.Object);
            sut.NextQuestion();

            // Act
            sut.SkipQuestion();


            // Assert
            Assert.Equal(svm.CorrectCount, 0);
            Assert.Equal(svm.ErrorCount, 0);
            Assert.Equal(svm.Skipped, true);

        }



        [Fact]
        public void Next_Should_Return_Null_When_All_Answered()
        {

            // Arrange
            Mock<ISpeachService> mockSpeachService = new Mock<ISpeachService>();
            List<SpellingViewModel> spellings = new List<SpellingViewModel>();
            Spelling s = new Spelling() { Word = "Dog", ContextSentence = "The dog and bone" };
            var svm = new SpellingViewModel(s);
            spellings.Add(svm);

            var sut = new SpellTestService(spellings, mockSpeachService.Object);
            var q = sut.NextQuestion();

            // Act
            q = sut.AnswerQuestion("Dog");

            // Assert
            Assert.Null(q);

            q = sut.NextQuestion();
            Assert.Null(q);

        }


        [Fact]
        public void Next_Should_Return_Null_When_All_Skipped()
        {

            Mock<ISpeachService> mockSpeachService = new Mock<ISpeachService>();
            List<SpellingViewModel> spellings = new List<SpellingViewModel>();
            Spelling s = new Spelling() { Word = "Dog", ContextSentence = "The dog and bone" };
            var svm = new SpellingViewModel(s);
            spellings.Add(svm);

            var sut = new SpellTestService(spellings, mockSpeachService.Object);
            var q = sut.NextQuestion();

            // Act
            q = sut.SkipQuestion();
            Assert.Null(q);
            q = sut.NextQuestion();

            // Assert
            Assert.Null(q);

        }


        [Fact]
        public void Next_Should_Return_Question_When_Answered_Incorrectly()
        {

            Mock<ISpeachService> mockSpeachService = new Mock<ISpeachService>();
            List<SpellingViewModel> spellings = new List<SpellingViewModel>();
            Spelling s = new Spelling() { Word = "Dog", ContextSentence = "The dog and bone" };
            var svm = new SpellingViewModel(s);
            spellings.Add(svm);

            var sut = new SpellTestService(spellings, mockSpeachService.Object);
            var q = sut.NextQuestion();

            // Act
            q = sut.AnswerQuestion("Dogg");

            Assert.NotNull(q);

            // Assert
            q = sut.AnswerQuestion("Dogg");
            Assert.NotNull(q);

        }


    }

}
