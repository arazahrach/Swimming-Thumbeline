using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction { Up, Down, Left, Right }

public class PipeUI : MonoBehaviour, IPointerClickHandler
{
    [Header("Pipe rotate status")]
    public bool canRotate = true;

    [Tooltip("Water Input direction")]
    public Direction[] inputDirections;

    [Tooltip("Water Output direction")]
    public Direction[] outputDirections;

    public int gridX;
    public int gridY;

    [Header("Level Flow")]
    public bool isSource = false;

    public bool isFinishPipe = false;


    private GridManager gridManager;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();

        if (gridManager != null)
        {
            gridManager.RegisterPipe(this);
        }
        else
        {
            Debug.LogError("GridManager not found");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, -90);
            RotateDirections(inputDirections);
            RotateDirections(outputDirections);
        }
    }

    void RotateDirections(Direction[] dirs)
    {
        for (int i = 0; i < dirs.Length; i++)
        {
            dirs[i] = GetNextDirection(dirs[i]);
        }
    }

    Direction GetNextDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: return Direction.Right;
            case Direction.Right: return Direction.Down;
            case Direction.Down: return Direction.Left;
            case Direction.Left: return Direction.Up;
        }
        return dir;
    }

    public bool HasInputFrom(Direction dir)
    {
        foreach (Direction d in inputDirections)
        {
            if (d == dir) return true;
        }
        return false;
    }
}
