using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GAMEPLAY,
        PAUSE
    }

    public GameState state;
    public bool stateChanged = false;
    public GameObject inventoryUI;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = GameState.GAMEPLAY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (stateChanged)
        {
            stateChanged = false;

            switch (state)
            {
                case GameState.GAMEPLAY:
                    Time.timeScale = 1.0f;
                    inventoryUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case GameState.PAUSE:
                    Time.timeScale = 0.0f;
                    inventoryUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    break;
            }
        }
    }

    public void PauseGame()
    {
        switch (state)
        {
            case GameState.GAMEPLAY:
                //if (Input.GetKeyDown(KeyCode.Escape))
                //{
                    state = GameState.PAUSE;
                    stateChanged = true;
                //}
                break;
            case GameState.PAUSE:
                //if (Input.GetKeyDown(KeyCode.Escape))
                //{
                    state = GameState.GAMEPLAY;
                    stateChanged = true;
                //}
                break;
        }
    }
}
