using Maze.Core.Models;

namespace Maze.Core.Services;

public class MazeParser
{
    public static bool TryParse(string filePath, out Models.Maze maze)
    { 
        maze = default;
        var lines = File.ReadAllLines(filePath);
        
        // Parse Size
        if (TryParseMazeSize(lines[0].Split(' '), out maze.MazeSize) == false)
        {
            return false;
        }

        // If Size zero
        if (maze.MazeSize.Height == 0 || maze.MazeSize.Width == 0)
        {
            return true;
        }
        
        var splitIndex = Array.IndexOf(lines, string.Empty);
        maze.RightWalls = new bool[maze.MazeSize.Height, maze.MazeSize.Width];
        maze.BottomWalls = new bool[maze.MazeSize.Height, maze.MazeSize.Width];

        // TryParse Right and Bottom walls
        bool isCorrect = ((splitIndex > 1 && splitIndex < lines.Length - 1)
                && TryParseBoolMatrix(lines.Skip(1).Take(splitIndex - 1).ToArray(), ref maze.RightWalls)
                && TryParseBoolMatrix(lines.Skip(splitIndex + 1).ToArray(), ref maze.BottomWalls));

        if (isCorrect == false)
        {
            maze.Clear();
        }
        return isCorrect;
    }
    
    private static bool TryParseBoolMatrix(string[] lines, ref bool[,] matrix)
    {
        if (lines.Length != matrix.GetLength(0))
        {
            return false;
        }

        for (int row = 0; row < lines.Length; row++)
        {
            bool[] inputMatrix;
            try
            {
                inputMatrix = Array.ConvertAll(lines[row].Split(' '), s => int.Parse(s) != 0);
            }
            catch (Exception e)
            {
                return false;
            }
            
            if (inputMatrix.Length != matrix.GetLength(1))
            {
                return false;
            }

            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                matrix[row, col] = inputMatrix[col];
            }
        }

        return true;
    }

    private static bool TryParseMazeSize(string[] sizeParts, out MazeSize mazeSize)
    {
        mazeSize = default;
        if (sizeParts.Length != 2) return false;
        
        return (int.TryParse(sizeParts[0], out mazeSize.Height) &&
                int.TryParse(sizeParts[1], out mazeSize.Width));
    }
}