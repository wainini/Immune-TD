using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wound : SelectablePlot
{
    [SerializeField] public int WoundStage { get; private set; }

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        WoundStage = 0;
    }

    public void CloseWound()
    {
        WoundStage++;

        anim.SetInteger("Stage", WoundStage);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentCursorState != CursorState.SelectWound)
        {
            sr.color = normalColor;
            return;
        }

        sr.color = hoverColor;

        plotManager.SetCurrentSelectedPlot(this);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentCursorState != CursorState.SelectWound)
        {
            sr.color = normalColor;
            return;
        }

        sr.color = normalColor;

        if (plotManager.CurrentSelectedPlot == this)
            plotManager.SetCurrentSelectedPlot(null);
    }

    public void DisableCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
