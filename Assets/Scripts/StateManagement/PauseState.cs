using UnityEngine;

public class PauseState : State
{
    public GameObject pauseUIGlobal;
    public override void Enter(GameObject pauseUI)
    {
        Time.timeScale = 0.0f;       
        Cursor.lockState = CursorLockMode.None;
        if (pauseUIGlobal == null)
        {
            pauseUIGlobal = pauseUI;
        }
        pauseUIGlobal.SetActive(true);
    }

    public override void Exit()
    {
        pauseUIGlobal.SetActive(false);
    }
}
