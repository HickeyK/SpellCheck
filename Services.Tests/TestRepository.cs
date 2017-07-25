using System;
using System.Collections.Generic;
using System.Diagnostics;
using SpellCheck;
using SpellCheck.Entities;
using Xunit;

namespace Services.Tests
{
    public class TestRepository
    {
        [Fact]
        public void ShouldSaveTestOccurance()
        {
            IConnectedRepository repo = new ConnectedRepository();
            var testOccurance = new TestOccurance
            {
                SpellTestId = 1,
                DateTestTaken = DateTime.Now
            };
            repo.SaveTestOccurance(testOccurance);
            Debug.Print(testOccurance.Id.ToString());
        }
        [Fact]
        public void ShouldSaveTestAnswer()
        {
            IConnectedRepository repo = new ConnectedRepository();
            var testAnswer = new TestAnswer()
            {
                AnswerStatus = "SUCCESS",
                FinalAnswer =  "spelling",
                NumberOfTries =  1,
                SpellTestId =  1,
                TestOccuranceId = 5
            };
            var testAnswers = new List<TestAnswer> {testAnswer};
            repo.SaveTestAnswers(testAnswers);

        }
    }
}
