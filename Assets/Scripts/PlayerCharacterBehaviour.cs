using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterBehaviour : ThirdPersonController
{
    [SerializeField] private GameManager gameManager;

    public InventoryBase inventory;

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

    private void OnOpenInventory(InputValue value)
    {
        if (value.isPressed)
        {
            gameManager.OpenInventory();
        }
    }

    //DEPRECATED - moved back to OnControllerColliderHit to allow for collisions and gravity at the same time
    //Trigger on collider removes any actual collision effect.
    //https://discussions.unity.com/t/collider-with-both-collision-and-triggering-choosing-at-runtime/571572/2
    //public void OnTriggerEnter(Collider other)
    //{
    //    var item = other.GetComponentInParent<CollectibleObject>();
    //    if (item)
    //    {
    //        inventory.AddItem(item.item, 1);
    //        Destroy(other.gameObject);
    //    }
    //}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var item = hit.gameObject.GetComponent<CollectibleObject>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(hit.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        //inventory.items.Clear();
    }

}
