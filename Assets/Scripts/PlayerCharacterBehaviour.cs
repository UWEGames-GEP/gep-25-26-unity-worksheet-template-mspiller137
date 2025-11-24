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

    private void OnRemoveItem(InputValue value)
    {
        if (value.isPressed)
        {
            Inventory inventory = FindAnyObjectByType<Inventory>();
            Debug.Log("Remove Item");
            inventory.Remove();
        }
    }
}
