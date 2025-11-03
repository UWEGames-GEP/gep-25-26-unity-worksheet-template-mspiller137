using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private List<string> items = new List<string>();
    public List<string> _items = new List<string>();



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
                AddItem("Sword");
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Remove("Sword");
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                AddItem("Helmet");
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                Remove("Helmet");
            }
        }

        _items = items;

        //When items is List<Object>, can use .sort(Comparison<T>) to sort by individual attributes
        _items.Sort();
    }

    private void AddItem(string itemName)
    {
        items.Add(itemName);
    }

    private void Remove(string itemName)
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
