using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CursorState
{
    Default,
    SelectUnit,
    SelectPlot,
    SelectWound
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Action<int> OnLivesChange;
    public Action<int> OnStemCellsChange;
    public Action OnEnemySpawnTimer;

    [field: SerializeField] public PlotManager plotManager { get; private set; }

    [SerializeField] private List<Wound> wounds = new List<Wound>();

    [Header("Game Stats")]
    [SerializeField] private int liveCount = 3;
    [SerializeField] private float stemCellsRegenSpeed = 0.2f;
    [SerializeField] private float enemySpawnCooldown = 2f;

    private int stemCells = 0;
    private float stemCellsTimer = 0f;
    private float inGameTime = 0f;
    private float currSpawnCooldown;

    public float InGameTime { get { return inGameTime; } }
    
    public int StemCells { get { return stemCells; } }
    public CursorState CurrentCursorState { get; private set; }

    public bool IsGamePaused { get; private set; }

    public bool IsGameEnded { get;private set; }
    private void Awake()
    {
        if(Instance != this)
        {
            Instance = this;
        }
        
        CurrentCursorState = CursorState.Default;
        stemCellsTimer = stemCellsRegenSpeed;
        inGameTime = 0f;
        currSpawnCooldown = 2f;
        IsGameEnded = false;
        PauseGame(false);
    }

    public void AddWound(Wound w)
    {
        wounds.Add(w);
    }

    private void FixedUpdate()
    {
        inGameTime += Time.deltaTime;
        stemCellsTimer -= Time.deltaTime;
        currSpawnCooldown -= Time.deltaTime;
        if(stemCellsTimer <= 0f)
        {
            stemCells += 1;
            stemCellsTimer = stemCellsRegenSpeed;
            OnStemCellsChange?.Invoke(stemCells);
        }
        if(currSpawnCooldown <= 0f)
        {
            currSpawnCooldown = enemySpawnCooldown;
            OnEnemySpawnTimer?.Invoke();
        }

        bool win = true;
        foreach(Wound w in wounds)
        {
            if (w.WoundStage != 3)
            {
                win = false;
            }
        }

        if (win) YouWin();
    }

    public void DecreaseLive()
    {
        liveCount -= 1;
        OnLivesChange?.Invoke(liveCount);
        if (liveCount <= 0)
        {
            GameOver();
        }
    }

    public void UseStemCells(int amountUsed)
    {
        stemCells = Mathf.Clamp(stemCells-amountUsed, 0, 9999);
        OnStemCellsChange?.Invoke(stemCells);
    }

    public void SetCurrentCursorState(CursorState state)
    {
        CurrentCursorState = state;
    }

    public void SetCurrentCursorStateWithDelay(CursorState state, float delay)
    {
        StartCoroutine(SetCursorStateDelayed(state, delay));
    }

    private IEnumerator SetCursorStateDelayed(CursorState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        CurrentCursorState = state;
    }

    public void PauseGame(bool pause)
    {
        IsGamePaused = pause;
        Time.timeScale = pause ? 0f : 1f;
    }

    private void GameOver()
    {
        MenuManager.Instance.OpenMenu("GameOver");
        IsGameEnded = true;
    }

    private void YouWin()
    {
        MenuManager.Instance.OpenMenu("YouWin");
        IsGameEnded = true;
    }


}


