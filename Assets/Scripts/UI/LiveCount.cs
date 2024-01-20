using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LiveCount : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnLivesChange += UpdateLiveCount;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLivesChange -= UpdateLiveCount;
    }

    private void UpdateLiveCount(int amount)
    {
        text.text = "Lives: " + amount + "/3";
    }
}
