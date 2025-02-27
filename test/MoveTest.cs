using TimHanewich.Chess;
using TimHanewich.Chess.PGN;
using Xunit;

namespace test
{
    public class MoveTest
    {        
        [Fact]
        public void CreateMove()
        {
            var board = BoardPosition.NewGame();
            var move = new Move("e4", board);
            Assert.Equal(Position.E2, move.FromPosition);
            Assert.Equal(Position.E4, move.ToPosition);
        }

        [Theory]
        [InlineData("game1.pgn", "1r5r/5Qk1/1p2b1p1/pPp1P1q1/P2p4/3P2P1/1P4B1/4RRK1 b - - 0 30")]
        [InlineData("game2.pgn", "8/8/4R1p1/2k3p1/1p4P1/1P1b1P2/3K1n2/8 b - - 2 43")]
        public void ExecuteGame(string gameFile, string expectedFen)
        {
            var board = BoardPosition.NewGame();
            string content = System.IO.File.ReadAllText($"../../../{gameFile}");
            PgnFile pgn = PgnFile.ParsePgn(content);

            foreach (var moveString in pgn.Moves)
            {
                var move = new Move(moveString, board);
                board.ExecuteMove(move);
            }

            Assert.Equal(expectedFen, board.ToFEN());
        }
    }
}
