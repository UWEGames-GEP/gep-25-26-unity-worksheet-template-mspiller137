using UnityEngine;

public class InventoryState : State
{
    private GameObject inventoryUIGlobal;
    public override void Enter(GameObject inventoryUI)
    {
        //Debug.Log("inventory enter");
        Time.timeScale = 0.0f;        
        Cursor.lockState = CursorLockMode.None;
        if(inventoryUIGlobal == null)
        {
            inventoryUIGlobal = inventoryUI;
        }
        inventoryUIGlobal.SetActive(true);
    }

    public override void Exit()
    {
        inventoryUIGlobal.SetActive(false);
    }
}
