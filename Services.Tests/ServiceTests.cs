using SpellCheck.Services;
using SpellCheck.Entities;
using SpellCheck.ViewModel;
using System.Collections.Generic;
using Xunit;

namespace Services.Tests
{
    public class ServiceTests
    {
        [Fact]
        public void Next_Should_Return_Null_When_All_Answered()
        {
            var spellings = new List<SpellingViewModel>();

            spellings.Add(new SpellingViewModel(
                new Spelling() { Word = "Dog", ContextSentence = "The dog and bone" })
                );

            var sut = new SpellTestService(spellings);

            var q = sut.NextQuestion();

            Assert.Equal(q.Word, "Dog");

            q = sut.AnswerQuestion("Dog");

            Assert.Null(q);

            q = sut.NextQuestion();

            Assert.Null(q);


        }


        [Fact]
        public void Next_Should_Return_Null_When_All_Skipped()
        {
            var spellings = new List<SpellingViewModel>();

            spellings.Add(new SpellingViewModel(
                new Spelling() { Word = "Dog", ContextSentence = "The dog and bone" })
                );

            var sut = new SpellTestService(spellings);

            var q = sut.NextQuestion();

            Assert.Equal(q.Word, "Dog");

            q = sut.SkipQuestion();

            Assert.Null(q);

            q = sut.NextQuestion();

            Assert.Null(q);


        }
    }

}
