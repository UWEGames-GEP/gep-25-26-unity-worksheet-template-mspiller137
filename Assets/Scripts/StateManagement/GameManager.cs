using UnityEngine;

public class GameManager : MonoBehaviour
{
    public State currentState;
    private GameplayState gameplayState = new GameplayState();
    private PauseState pauseState = new PauseState();
    private InventoryState inventoryState = new InventoryState();

    //public bool stateChanged = false;
    public GameObject inventoryUI;
    public GameObject pauseUI;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = gameplayState;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        //if (stateChanged)
        //{
        //    stateChanged = false;

        //    switch (currentState)
        //    {
        //        case gameplayState:
        //            Time.timeScale = 1.0f;
        //            inventoryUI.SetActive(false);
        //            Cursor.lockState = CursorLockMode.Locked;
        //            break;
        //        case GameState.PAUSE:
        //            Time.timeScale = 0.0f;
        //            inventoryUI.SetActive(true);
        //            Cursor.lockState = CursorLockMode.None;
        //            break;
        //    }
        //}
    }

    public void PauseGame()
    {
        //Debug.Log("pause trigger");
        State prev = currentState;
        prev.Exit();
        switch (currentState)
        {
            //https://stackoverflow.com/questions/7593377/switch-case-in-c-sharp-a-constant-value-is-expected
            case var value when value == gameplayState:
                currentState = pauseState;
                currentState.Enter(pauseUI);
                break;
            case var value when value == pauseState:
                currentState = gameplayState;
                currentState.Enter();
                break;
            case var value when value == inventoryState:
                currentState = gameplayState;
                currentState.Enter();
                break;
        }
        
        
    }

    public void OpenInventory()
    {
        State prev = currentState;
        prev.Exit();
        switch (currentState)
        {
            //https://stackoverflow.com/questions/7593377/switch-case-in-c-sharp-a-constant-value-is-expected
            case var value when value == gameplayState:
                currentState = inventoryState;
                currentState.Enter(inventoryUI);
                break;
            case var value when value == pauseState:
                //cannot access inventory from pause menu
                break;
            case var value when value == inventoryState:
                currentState = gameplayState;
                currentState.Enter();
                break;
        }
        //Debug.Log("pre inventory enter");        
    }

    public void Resume()
    {
        currentState.Exit();
        currentState = gameplayState;
        currentState.Enter();
    }
}
