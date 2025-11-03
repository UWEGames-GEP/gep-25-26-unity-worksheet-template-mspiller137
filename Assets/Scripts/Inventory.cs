using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    public List<string> items = new List<string>();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.state == GameManager.GameState.GAMEPLAY)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                AddItem("sword");
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Remove("sword");
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                AddItem("Shield");
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                Remove("Shield");
            }
        }
        
    }

    public void AddItem(string itemName)
    {
        items.Add(itemName);
    }

    public void Remove(string itemName)
    {
        items.Remove(itemName);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        CollectibleObject collisionItem = hit.gameObject.GetComponent<CollectibleObject>();

        if (collisionItem != null)
        {
            items.Add(collisionItem.name);
            Destroy(collisionItem.gameObject);
        }
    }

}
