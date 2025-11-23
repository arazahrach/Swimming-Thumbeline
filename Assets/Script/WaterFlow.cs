using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class WaterFlow : MonoBehaviour
{
    public GridManager gridManager;

    [Header("Source Pipe")]
    public PipeUI startPipe;

    [Header("Water Direction")]
    public Direction startDirection = Direction.Up;
    
    public GameState state = GameState.Editing;  
    public GameObject winUI; 
    public GameObject loseUI;  

    public void TriggerFlow()
    {
        if (startPipe == null)
        {
            Debug.LogError("StartPipe is not choosen yet");
            return;
        }

        state = GameState.Running;

        Debug.Log("Flow start");
        StartFlow(startPipe, startDirection);
    }

    public void StartFlow(PipeUI startPipe, Direction fromDirection)
    {
        Flow(startPipe, fromDirection);
    }

    bool Flow(PipeUI currentPipe, Direction from)
    {
    Image pipeImage = currentPipe.GetComponent<Image>();
        if (pipeImage != null)
        {
            pipeImage.color = Color.blue;
        }

        if (currentPipe.isFinishPipe)
        {
            Debug.Log("You Win!");
            state = GameState.Ended;

            if (winUI != null)
                winUI.SetActive(true);

            return true;
        }

        Debug.Log($"Current Pipe: ({currentPipe.gridX},{currentPipe.gridY}) OutDirs: {string.Join(",", currentPipe.outputDirections)} From: {from}");

        List<PipeUI> nextPipes = new List<PipeUI>();

        foreach (Direction outDir in currentPipe.outputDirections)
        {
            if (outDir != Opposite(from))
            {
                PipeUI neighbor = gridManager.GetNeighbor(currentPipe, outDir);

                if(neighbor == null)
                {
                    Debug.Log($"Neighbor in direction {outDir} is NULL");
                }
                else
                {
                    bool hasInput = neighbor.HasInputFrom(Opposite(outDir));
                    Debug.Log($"Neighbor found at ({neighbor.gridX},{neighbor.gridY}) InputDirs: {string.Join(",", neighbor.inputDirections)} HasInputFrom {Opposite(outDir)} = {hasInput}");

                    if (hasInput)
                        nextPipes.Add(neighbor);
                }
            }
        }

 
        if (nextPipes.Count == 0)
        {
            Debug.Log("Game Over");
            state = GameState.Ended;

            if (loseUI != null)
                loseUI.SetActive(true);

            return false;
        }

        PipeUI chosen = null;
        foreach (PipeUI p in nextPipes)
        {
            if (IsCorner(p))
            {
                chosen = p;
                break;
            }
        }

        if (chosen == null)
        {
            chosen = nextPipes[0]; 
        }

        return Flow(chosen, GetDirection(currentPipe, chosen));
    }


    Direction Opposite(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: return Direction.Down;
            case Direction.Down: return Direction.Up;
            case Direction.Left: return Direction.Right;
            case Direction.Right: return Direction.Left;
        }
        return dir;
    }

    bool IsCorner(PipeUI pipe)
    {
        if (pipe.outputDirections.Length != 2) return false;

        Direction a = pipe.outputDirections[0];
        Direction b = pipe.outputDirections[1];

        if ((a == Direction.Up && b == Direction.Down) ||
            (a == Direction.Down && b == Direction.Up) ||
            (a == Direction.Left && b == Direction.Right) ||
            (a == Direction.Right && b == Direction.Left))
            return false;

        return true; 
    }

    Direction GetDirection(PipeUI from, PipeUI to)
    {
    if (to.gridX > from.gridX) return Direction.Right;
    if (to.gridX < from.gridX) return Direction.Left;
    if (to.gridY > from.gridY) return Direction.Down; 
    if (to.gridY < from.gridY) return Direction.Up;
    
    return Direction.Up;
    }   

}
