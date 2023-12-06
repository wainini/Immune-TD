using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectablePlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private Color normalColor;

    private PlotManager plotManager;

    private Unit unitOnPlot;

    public bool IsOccupied => unitOnPlot != null;

    private void Start()
    {
    }

    public void InitializePlot(PlotManager manager)
    {
        plotManager = manager;
        normalColor = sr.color;
        unitOnPlot = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentCursorState != CursorState.SelectPlot) return;

        sr.color = hoverColor;

        plotManager.SetCurrentSelectedPlot(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentCursorState != CursorState.SelectPlot) return;

        sr.color = normalColor;

        if(plotManager.CurrentSelectedPlot == this)
            plotManager.SetCurrentSelectedPlot(null);
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
