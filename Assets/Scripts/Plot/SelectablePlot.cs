using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectablePlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private Color normalColor;

    private Unit unitOnPlot;

    public bool IsOccupied => unitOnPlot != null;

    private void Start()
    {
        normalColor = sr.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentCursorState != CursorState.SelectPlot) return;

        sr.color = hoverColor;

        PlotManager.Instance.SetCurrentSelectedPlot(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentCursorState != CursorState.SelectPlot) return;

        sr.color = normalColor;

        if(PlotManager.Instance.CurrentSelectedPlot == this)
            PlotManager.Instance.SetCurrentSelectedPlot(null);
    }

    public void SetUnit(Unit unit)
    {
        unitOnPlot = unit;
    }

    public void RemoveUnit()
    {
        unitOnPlot = null;
    }
}
