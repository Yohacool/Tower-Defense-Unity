using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Menu,
        Playing,
        Win,
        Lose
    }

    public GameState state;

    public GameObject menuUI;
    public GameObject winUI;
    public GameObject loseUI;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetState(GameState.Menu);
    }

    public void StartGame()
    {
        SetState(GameState.Playing);
    }

    public void WinGame()
    {
        SetState(GameState.Win);
    }

    public void LoseGame()
    {
        SetState(GameState.Lose);
    }

    void SetState(GameState newState)
    {
        state = newState;

        menuUI.SetActive(state == GameState.Menu);
        winUI.SetActive(state == GameState.Win);
        loseUI.SetActive(state == GameState.Lose);

        Time.timeScale = (state == GameState.Playing) ? 1f : 0f;
    }
}