using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameState gameState = GameState.MAIN_MENU;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }
}

public enum GameState
{
    IN_GAME, PAUSED, LEVEL_COMPLETE, GAME_OVER, TUTORIAL, MAIN_MENU
}
