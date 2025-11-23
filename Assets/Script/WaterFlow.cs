using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WaterFlow : MonoBehaviour
{
    public GridManager gridManager;

    [Header("Auto Start Timer")]
    public bool useAutoStart = true;
    public float countdown = 30f;
    public Text countdownText;

    [Header("Fairy")]
    public Transform fairy;
    public float moveDuration = 0.5f;

    [Header("Source Pipe")]
    public PipeUI startPipe;

    [Header("Water Direction")]
    public Direction startDirection = Direction.Up;
    public GameState state = GameState.Editing;
    public GameObject winUI;
    public GameObject loseUI;

    void Start()
    {
        if (useAutoStart)
            StartCoroutine(StartCountdown());
    }

    void Awake()
    {
        if (gridManager == null)
            gridManager = FindObjectOfType<GridManager>();

        if (fairy == null)
            fairy = GameObject.Find("Fairy")?.transform;

        if (startPipe == null)
            startPipe = FindObjectOfType<PipeUI>();

        if (countdownText == null)
            countdownText = FindObjectOfType<Text>();
    }



    IEnumerator StartCountdown()
    {
        float t = countdown;

        while (t > 0 && state == GameState.Editing)
        {
            if (countdownText != null)
                countdownText.text = Mathf.CeilToInt(t).ToString();

            t -= Time.deltaTime;
            yield return null;
        }

        if (state == GameState.Editing)
            TriggerFlow();
    }


    public GameObject Nextlevel;

    public void TriggerFlow()
    {
        if (startPipe == null)
            return;

        state = GameState.Running;

        fairy.position = startPipe.transform.position;

        StartFlow(startPipe, startDirection);
    }

    public void StartFlow(PipeUI startPipe, Direction fromDirection)
    {
        StartCoroutine(Flow(startPipe, fromDirection));
    }

    IEnumerator Flow(PipeUI currentPipe, Direction from)
    {
        Image pipeImage = currentPipe.GetComponent<Image>();
        if (pipeImage != null)
            pipeImage.color = Color.blue;

        if (currentPipe.isFinishPipe)
        {
            state = GameState.Ended;
            if (winUI != null) winUI.SetActive(true);
            yield break;
        }

        List<PipeUI> nextPipes = new List<PipeUI>();

        foreach (Direction outDir in currentPipe.outputDirections)
        {
            if (outDir != Opposite(from))
            {
                PipeUI neighbor = gridManager.GetNeighbor(currentPipe, outDir);

                if (neighbor != null)
                {
                    bool hasInput = neighbor.HasInputFrom(Opposite(outDir));
                    if (hasInput)
                        nextPipes.Add(neighbor);
                }
            }
        }

        if (nextPipes.Count == 0)
        {
            state = GameState.Ended;
            if (loseUI != null) loseUI.SetActive(true);
            yield break;
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
            chosen = nextPipes[0];

        // perubahan
        yield return StartCoroutine(MoveFairy(currentPipe, chosen));

        yield return StartCoroutine(Flow(chosen, GetDirection(currentPipe, chosen)));
    }

    IEnumerator MoveFairy(PipeUI from, PipeUI to)
    {
        Vector3 startPos = from.transform.position;
        Vector3 endPos = to.transform.position;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration;
            fairy.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
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
