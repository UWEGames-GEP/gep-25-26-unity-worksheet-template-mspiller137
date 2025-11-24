using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterBehaviour : ThirdPersonController
{
    [SerializeField] private GameManager gameManager;

    private void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            gameManager.PauseGame();
        }
    }
}
