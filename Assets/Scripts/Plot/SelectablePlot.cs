using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectablePlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected Color hoverColor;
    protected Color normalColor;

    protected PlotManager plotManager;

    protected Unit unitOnPlot;

    public bool IsOccupied => unitOnPlot != null;

    private void Start()
    {
    }

    public virtual void InitializePlot(PlotManager manager)
    {
        plotManager = manager;
        normalColor = sr.color;
        unitOnPlot = null;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentCursorState != CursorState.SelectPlot)
        {
            sr.color = normalColor;
            return;
        }

        sr.color = hoverColor;

        plotManager.SetCurrentSelectedPlot(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentCursorState != CursorState.SelectPlot)
        {
            sr.color = normalColor;
            return;
        }

        sr.color = normalColor;

        if(plotManager.CurrentSelectedPlot == this)
            plotManager.SetCurrentSelectedPlot(null);
    }

    public void SetUnit(Unit unit)
    {
        unitOnPlot = unit;
        sr.color = normalColor;
    }

    public void RemoveUnit()
    {
        unitOnPlot = null;
    }
}
