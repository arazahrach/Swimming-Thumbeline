using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
        public int rows = 6;
        public int columns = 10;
        public PipeUI[,] grid;
        public void RegisterPipe(PipeUI pipe)

    {
        if(grid == null) grid = new PipeUI[rows, columns];

        if(pipe.gridY < 0 || pipe.gridY >= rows || pipe.gridX < 0 || pipe.gridX >= columns)
        {
            Debug.LogError($"Pipe on ({pipe.gridX},{pipe.gridY}) is out from the grid limit");
            return;
        }

        grid[pipe.gridY, pipe.gridX] = pipe;
    }

    public PipeUI GetNeighbor(PipeUI pipe, Direction dir)
    {
        int x = pipe.gridX;
        int y = pipe.gridY;

        switch(dir)
        {
            case Direction.Up: y -= 1; break;   
            case Direction.Down: y += 1; break; 
            case Direction.Left: x -= 1; break;
            case Direction.Right: x += 1; break;
        }

        if (x < 0 || x >= columns || y < 0 || y >= rows)
            return null;

        return grid[y, x];
    }

    public PipeUI[] AllPipes
    {
        get
        {
            List<PipeUI> list = new List<PipeUI>();
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (grid[y, x] != null) list.Add(grid[y, x]);
                }
            }
            return list.ToArray();
        }
    }
}
