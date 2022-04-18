using GameOfLife.Data;
using Xunit;

namespace GameOfLife.Tests
{
    public class GameBoardTests
    {
        [Theory]
        [InlineData(true, 0, false)]
        [InlineData(true, 1, false)]
        [InlineData(true, 2, true)]
        [InlineData(true, 3, true)]
        [InlineData(true, 4, false)]
        [InlineData(true, 5, false)]
        [InlineData(true, 6, false)]
        [InlineData(true, 7, false)]
        [InlineData(true, 8, false)]
        [InlineData(false, 0, false)]
        [InlineData(false, 1, false)]
        [InlineData(false, 2, false)]
        [InlineData(false, 3, true)]
        [InlineData(false, 4, false)]
        [InlineData(false, 5, false)]
        [InlineData(false, 6, false)]
        [InlineData(false, 7, false)]
        [InlineData(false, 8, false)]
        public void CellsFollowsRulesOnAdvancingGeneration(bool initialState, int neighbours, bool advancedState)
        {
            var sut = CreateSystemUnderTest();

            if (initialState)
            {
                sut.ToggleCell(4);
            }

            Assert.Equal(initialState, sut.Board[4]);

            var neighbourIndex = new[] { 0, 1, 2, 3, 5, 6, 7, 8 };

            for (var i = 0; i < neighbours; i++)
            {
                sut.ToggleCell(neighbourIndex[i]);
            }

            sut.AdvanceGeneration();

            Assert.Equal(advancedState, sut.Board[4]);

        }

        private GameBoard CreateSystemUnderTest(int maxX = 3, int maxY = 3)
        {
            return new GameBoard(maxX, maxY);
        }
    }
}