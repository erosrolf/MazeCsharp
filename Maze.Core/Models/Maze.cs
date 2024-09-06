namespace Maze.Core.Models;

public struct MazeSize
{
    public MazeSize(int x, int y)
    {
        Width = x;
        Height = y;
    }
    
    public int Width;
    public int Height;
}

public struct Maze
{
    public MazeSize MazeSize;
    public bool[,] RightWalls;
    public bool[,] BottomWalls;

    public Maze(int width, int height)
    {
        MazeSize = new MazeSize(width, height);
        RightWalls = new bool[height, width];
        BottomWalls = new bool[height, width];
    }

    public void Clear()
    {
        MazeSize.Width = MazeSize.Height = 0;
        RightWalls = new bool[MazeSize.Height, MazeSize.Width];
        BottomWalls = new bool[MazeSize.Height, MazeSize.Width];
    }

    public bool GetHorizontalWall(int x, int y)
    {
        if (y == 0 || y == MazeSize.Height) return true;
        
        if (x > MazeSize.Width || y > MazeSize.Height || x == 0) return false;
        return BottomWalls[y - 1, x - 1];
    }

    public bool GetVerticalWall(int x, int y)
    {
        if (x == 0 || x == MazeSize.Width) return true;
        
        if (x > MazeSize.Width || y > MazeSize.Height || y == 0) return false;
        return RightWalls[y - 1, x - 1];
    }
}