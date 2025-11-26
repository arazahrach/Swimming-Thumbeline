using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private PipeUI blockedPipe;
    private Direction[] cachedInputDirections;
    private Color originalPipeColor;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    private GridManager gridManager;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        gridManager = FindObjectOfType<GridManager>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        if (blockedPipe != null)
        {
            blockedPipe.inputDirections = cachedInputDirections;

            Image img = blockedPipe.GetComponent<Image>();
            if (img != null)
                img.color = originalPipeColor;

            blockedPipe = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        PipeUI targetPipe = GetPipeUnder();
        if (targetPipe != null)
        {
            blockedPipe = targetPipe;
            cachedInputDirections = targetPipe.inputDirections;
            rectTransform.position = targetPipe.transform.position;

            targetPipe.inputDirections = new Direction[0];

            Image pipeImg = targetPipe.GetComponent<Image>();
            if (pipeImg != null)
            {
                originalPipeColor = pipeImg.color;
                pipeImg.color = new Color(0.7f, 0.7f, 0.7f); 
            }
        }

        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    private PipeUI GetPipeUnder()
    {
        float minDist = float.MaxValue;
        PipeUI result = null;

        foreach (PipeUI p in gridManager.AllPipes)
        {
            float dist = Vector3.Distance(transform.position, p.transform.position);
            if (dist < 60f && dist < minDist)
            {
                minDist = dist;
                result = p;
            }
        }
        return result;
    }
}
