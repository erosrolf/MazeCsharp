using System.Numerics;

namespace Maze.Core.Models;

public struct Maze
{
    public Vector2 Size;
    public bool[,] RightWalls;
    public bool[,] BottomWalls;
}