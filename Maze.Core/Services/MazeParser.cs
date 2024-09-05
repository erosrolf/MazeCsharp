namespace Maze.Core.Services;

public class MazeParser
{
    public static bool TryParse(string filePath, out Models.Maze maze)
    { 
        maze = default;
        var lines = File.ReadAllLines(filePath);
        
        // Parse Size
        if (TryParseMazeSize(lines[0].Split(' '), out maze.Size) == false)
        {
            return false;
        }

        // If Size zero
        if (maze.Size.Item1 == 0 || maze.Size.Item2 == 0)
        {
            return true;
        }
        
        var splitIndex = Array.IndexOf(lines, string.Empty);
        maze.RightWalls = new bool[maze.Size.Item1, maze.Size.Item2];
        maze.BottomWalls = new bool[maze.Size.Item1, maze.Size.Item2];

        // TryParse Right and Bottom walls
        return ((splitIndex > 1 && splitIndex < lines.Length - 1)
                && TryParseBoolMatrix(lines.Skip(1).Take(splitIndex - 1).ToArray(), ref maze.RightWalls)
                && TryParseBoolMatrix(lines.Skip(splitIndex + 1).ToArray(), ref maze.BottomWalls));
    }
    
    private static bool TryParseBoolMatrix(string[] lines, ref bool[,] matrix)
    {
        if (lines.Length != matrix.GetLength(0))
        {
            return false;
        }

        for (int row = 0; row < lines.Length; row++)
        {
            var inputMatrix = Array.ConvertAll(lines[row].Split(' '), s => int.Parse(s) != 0);
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

    private static bool TryParseMazeSize(string[] sizeParts, out (int, int) size)
    {
        size = default;
        if (sizeParts.Length != 2) return false;
        
        return (int.TryParse(sizeParts[0], out size.Item1) &&
                int.TryParse(sizeParts[1], out size.Item2));
    }
}