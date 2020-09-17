using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.WindowManagement;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MentorSpeedDatingAppTest
{
    public class TestWindowManager
    {
        [Fact]
        public void TestSplitMatchingsIntoSmallerMatchings()
        {
            #region Arrange

            var testlist = new List<Matching>();
            for (var i = 0; i < 10; i++)
            {
                testlist.Add(new Matching());
            }

            #endregion Arrange

            #region Act

            var splitInTwoTest = WindowManager.SplitMatchingsIntoSmallerMatchings(testlist, 5);
            var splitInFiveTest = WindowManager.SplitMatchingsIntoSmallerMatchings(testlist, 2);
            var unevenSplit = WindowManager.SplitMatchingsIntoSmallerMatchings(testlist, 3);

            #endregion Act

            #region Assert

            Assert.Equal(2, splitInTwoTest.Count);
            Assert.Equal(5, splitInFiveTest.Count);
            Assert.Equal(4, unevenSplit.Count);
            Assert.Single(unevenSplit.Last());

            #endregion Assert
        }
    }
}