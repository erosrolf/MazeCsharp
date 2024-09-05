namespace Maze.Core.Models;

public struct Maze
{
    public (int, int) Size;
    public bool[,] RightWalls;
    public bool[,] BottomWalls;
}