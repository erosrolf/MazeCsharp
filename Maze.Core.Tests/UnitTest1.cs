using System.Collections;

namespace Maze.Core.Tests;

public class MazeParserTests
{
    [Fact]
    public void Parse_ValidMazeFile_ReturnsCorrectStructure_IfInputIsCorrect()
    {
        // Arrange
        string filePath = "TestFiles/ValidMazeFile01.txt";

        // Act
        bool isParsed = MazeParser.TryParse(filePath, out var maze);

        //Assert
        Assert.True(isParsed);
        Assert.Equal(new ValueTuple<int, int>(4, 4), maze.Size);

        bool[,] expectedRightWalls = new bool[4, 4]
        {
            { false, false, false, true },
            { true, false, true, true },
            { false, true, false, true },
            { false, false, false, true }
        };
        Assert.True(AreMatricesEqual(maze.RightWalls, expectedRightWalls));

        bool[,] expectedBottomWalls = new bool[4, 4]
        {
            { true, false, true, false },
            { false, false, true, false },
            { true, true, false, true },
            { true, true, true, true }
        };
        Assert.True(AreMatricesEqual(maze.BottomWalls, expectedBottomWalls));
    }
    
    private bool AreMatricesEqual(bool[,] matrix1, bool[,] matrix2)
    {
        if (matrix1.GetLength(0) != matrix2.GetLength(0) || 
            matrix1.GetLength(1) != matrix2.GetLength(1))
        {
            return false;
        }

        for (int i = 0; i < matrix1.GetLength(0); i++)
        {
            for (int j = 0; j < matrix1.GetLength(1); j++)
            {
                if (matrix1[i, j] != matrix2[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }
}