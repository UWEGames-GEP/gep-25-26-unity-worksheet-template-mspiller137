using UnityEngine;

public class GameplayState : State
{
    public override void Enter()
    {
        Time.timeScale = 1.0f;
        inventoryUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Exit()
    {

    }
}
