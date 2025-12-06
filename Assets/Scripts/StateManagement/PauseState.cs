using UnityEngine;

public class PauseState : State
{
    public override void Enter()
    {
        Time.timeScale = 0.0f;
        inventoryUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Exit()
    {

    }
}
