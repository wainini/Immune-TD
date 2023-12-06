using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public Action OnEscape;

    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
        else
        {
            Instance = this;
        }
        #endregion
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape?.Invoke();
        }
    }
}
