namespace Maze.Core.Generators;

public class MazeGenerator
{
    public void GenerateMaze(int width, int height)
    {
        var maze = new Models.Maze(width, height);
        
        // генерация происходит по строкам сверху вниз для этого заведем счетчик
        int rowCounter = 0;
        
        // генерируем случайное множество в количестве столбцов
        int[] set = Enumerable.Range(0, width).ToArray();
        
        // начинаем генерацию правых стенок,
        // для каждого элемента в строке либо ставим слену справа,
        // либо объединяем его множество с множеством справа
        for (int col = 0; col < maze.MazeSize.Width; col++)
        {
            var WillPut = RandomDecision();
            if (WillPut)
            {
                maze.RightWalls[rowCounter, col] = true;
            }
            else
            {
                UnionToRightSet(ref set, col);
            }
        }
        
        // начинаем ставить стены снизу
    }

    private void UnionToRightSet(ref int[] set, int appendableIndex)
    {
        var rightIndex = appendableIndex + 1;
        if (rightIndex >= set.Length) return;

        var oldValue = set[appendableIndex];
        var newValue = set[rightIndex];

        for (int i = 0; i < set.Length; i++)
        {
            if (set[i] == oldValue) set[i] = newValue;
        }
    }

    private bool RandomDecision()
    {
        return Random.Shared.NextDouble() < 0.5;
    }
}