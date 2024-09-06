namespace Maze.Core.Tests;

public class GetWallsTests
{
    #region TestFunctions
    private bool AreHorizontalWallsIsCorrectEqual(Models.Maze maze, bool[,] expectedMatrix)
    {
        for (int row = 0; row < expectedMatrix.GetLength(0); row++)
        {
            for (int col = 0; col < expectedMatrix.GetLength(1); col++)
            {
                if (maze.GetHorizontalWall(col, row) != expectedMatrix[row, col]) return false;
            }
        }
        return true;
    }
    
    private bool AreVerticalWallsIsCorrectEqual(Models.Maze maze, bool[,] expectedMatrix)
    {
        for (int row = 0; row < expectedMatrix.GetLength(0); row++)
        {
            for (int col = 0; col < expectedMatrix.GetLength(1); col++)
            {
                if (maze.GetVerticalWall(col, row) != expectedMatrix[row, col]) return false;
            }
        }
        return true;
    }
    
    #endregion
    
    [Fact]
    public void ParseValidMatrixFile_ReturnsCorrectHorizontalWalls()
    {
        // Arrage
        string filePath = "TestFiles/ValidMazeFile01.txt";
        
        // Act
        MazeParser.TryParse(filePath, out var maze);
        
        // Assert
        bool[,] expectedBottomWalls = new bool[5, 5]
        {
            { true, true, true, true, true },
            { false, true, false, true, false },
            { false, false, false, true, false },
            { false, true, true, false, true },
            { true, true, true, true, true }
        };
        Assert.True(AreHorizontalWallsIsCorrectEqual(maze, expectedBottomWalls));
    }
    
    [Fact]
    public void ParseValidMatrixFile_ReturnsCorrectVerticalWalls()
    {
        // Arrage
        string filePath = "TestFiles/ValidMazeFile01.txt";
        
        // Act
        MazeParser.TryParse(filePath, out var maze);
        
        // Assert
        bool[,] expectedRightWalls = new bool[5, 5]
        {
            {true, false, false, false, true },
            {true, false, false, false, true },
            {true, true, false, true, true },
            {true, false, true, false, true },
            {true, false, false, false, true }
        };
        Assert.True(AreVerticalWallsIsCorrectEqual(maze, expectedRightWalls));
    }
}