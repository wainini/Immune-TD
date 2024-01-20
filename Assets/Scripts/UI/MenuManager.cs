using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [SerializeField] private List<Menu> menuList;

    private Stack<Menu> menuStack = new Stack<Menu>();

    private List<Menu> gameHUD = new List<Menu>();

    private void Awake()
    {

        if (Instance == null && Instance != this)
        {
            Instance = this;
        }

        Screen.SetResolution(Screen.width, Screen.width / 16 * 9, true);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) return;
        if (GameManager.Instance.IsGameEnded) return;
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.CurrentCursorState == CursorState.Default)
        {
            if (menuStack.Count == 0)
            {
                OpenMenu("PauseMenu");
            }
            else if (menuStack.Count != 0)
            {
                CloseMenu();
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CloseAllMenu();
    }

    public Menu GetMenu(string name)
    {
        if (menuList.Count == 0)
        {
            return null;
        }
        return menuList.Find((x) => x.Name == name);
    }

    public void OpenMenu(string name)
    {
        var menu = GetMenu(name);
        if (menu == null)
        {
            Debug.Log(name + " is not found");
            return;
        }
        menu.Canvas.enabled = true;
        menuStack.Push(menu);

        if (menu.PauseWhenOpen)
            GameManager.Instance.PauseGame(true);
    }

    public void CloseMenu()
    {
        if (menuStack.Count == 0)
        {
            Debug.Log("MenuStack is empty");
            return;
        }

        menuStack.Pop().Canvas.enabled = false;

        if (menuStack.Count == 0)
            GameManager.Instance.PauseGame(false);
        else if (menuStack.Peek().PauseWhenOpen)
            GameManager.Instance.PauseGame(true);
        else
            GameManager.Instance.PauseGame(false);
    }

    public void CloseAllMenu()
    {
        if (menuStack.Count == 0) return;
        int count = menuStack.Count;
        for (int i = 0; i < count; i++)
        {
            CloseMenu();
        }
    }
}
