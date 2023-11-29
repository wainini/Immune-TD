using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorState
{
    Default,
    SelectUnit,
    SelectPlot
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CursorState CurrentCursorState { get; private set; }

    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        #endregion
        
        CurrentCursorState = CursorState.Default;
    }

    public void SetCurrentCursorState(CursorState state)
    {
        CurrentCursorState = state;
    }
}


