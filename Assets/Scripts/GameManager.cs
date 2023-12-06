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

    [field: SerializeField] public PlotManager plotManager { get; private set; }

    public CursorState CurrentCursorState { get; private set; }

    public bool IsGamePaused { get; private set; }

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
        
        CurrentCursorState = CursorState.Default;
    }

    public void SetCurrentCursorState(CursorState state)
    {
        CurrentCursorState = state;
    }

    public void PauseGame(bool pause)
    {
        IsGamePaused = pause;
        Time.timeScale = pause ? 0f : 1f;
    }
}


