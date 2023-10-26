using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectablePlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private Color normalColor;

    private void Start()
    {
        normalColor = sr.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sr.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sr.color = normalColor;
    }
}
