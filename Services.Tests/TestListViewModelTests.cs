using Moq;
using SpellCheck;
using SpellCheck.Entities;
using SpellCheck.Services;
using SpellCheck.ViewModel;
using System.Collections.ObjectModel;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using SpellCheck;

namespace ViewModel.Tests
{
    public class TestListViewModelTests
    {
        [Fact]
        public void ListContainsCorrectNumberOfSpellTests()
        {
            //Mock<IConnectedRepository> repo = new Mock<IConnectedRepository>();
            IConnectedRepository repo = new MockConectedRepository();
            var sut = new TestListViewModel(repo);

            var testList = sut.Tests;

            Assert.Equal(2, testList.Count);

            var test = sut.Tests.SingleOrDefault(f => f.Id ==  1);
            Assert.Equal("Test 1", test.Description);
            Assert.Equal(new DateTime(2017, 1, 1), test.DateCreated);

        }

        [Fact]
        public void SpellingsContainsCorrectMembersForCurrentTest()
        {
            IConnectedRepository repo = new MockConectedRepository();
            var sut = new TestListViewModel(repo);
            var testList = sut.Tests;
            var test = sut.Tests.SingleOrDefault(f => f.Id == 2);

            // Act
            sut.CurrentTest = test;

            // Assert
            Assert.Equal(3, sut.Spellings.Count);

            Assert.Contains(sut.Spellings, s => s.Word == "Five");


        }

    }


    class MockConectedRepository : IConnectedRepository
    {
        public void AddTest(SpellTest test)
        {
            throw new NotImplementedException();
        }

        public bool SaveTestOccurance(TestOccurance testOccurance)
        {
            throw new NotImplementedException();
        }

        public List<TestAnswer> GetAnswers(int testOccuranceId)
        {
            throw new NotImplementedException();
        }

        public void SaveTestAnswers(List<TestAnswer> testAnswers)
        {
            throw new NotImplementedException();
        }

        public List<Spelling> GetSpellings(int testId)
        {

            var spellTests = GetTests();
            var spellTest = spellTests.Where(st => st.Id == testId);
            return spellTest.FirstOrDefault().Spellings;
        }

        public List<TestOccurance> GetTestOccurances()
        {
            throw new NotImplementedException();
        }

        public List<TestOccurance> GetTestOccurances(int testId)
        {
            throw new NotImplementedException();
        }

        public List<SpellTest> GetTests()
        {
            var list = new List<SpellTest>();
            list.Add(new SpellTest
            {
                Id=1,
                Description = "Test 1",
                DateCreated = new DateTime(2017, 1, 1),
                Spellings = new List<Spelling> { new Spelling {Word = "One" }, new Spelling {Word = "Two" } }

            });

            list.Add(new SpellTest
            {
                Id = 2,
                Description = "Test 2",
                DateCreated = new DateTime(2017, 6, 30),
                Spellings = new List<Spelling> { new Spelling { Word = "Three" }, new Spelling { Word = "Four" }, new Spelling {Word="Five"} }
            });

            return list;
        }

        public void UpdateTest(SpellTest test)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
