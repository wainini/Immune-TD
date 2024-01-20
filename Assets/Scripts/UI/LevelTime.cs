using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTime : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = GameManager.Instance.InGameTime;

        text.text = $"{time / 60:00}:{time % 60:00}";
    }
}
