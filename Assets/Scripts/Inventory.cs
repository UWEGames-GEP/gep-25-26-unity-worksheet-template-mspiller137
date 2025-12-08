using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform collectiblesTransform;

    private List<CollectibleObject> items = new List<CollectibleObject>();
    public List<CollectibleObject> _items = new List<CollectibleObject>();    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if(gameManager.state == GameManager.GameState.GAMEPLAY)
        //{
        //    if (Input.GetKeyDown(KeyCode.O))
        //    {
        //        AddItem("Sword");
        //    }
        //    else if (Input.GetKeyDown(KeyCode.P))
        //    {
        //        Remove("Sword");
        //    }
        //    else if (Input.GetKeyDown(KeyCode.K))
        //    {
        //        AddItem("Helmet");
        //    }
        //    else if (Input.GetKeyDown(KeyCode.L))
        //    {
        //        Remove("Helmet");
        //    }
        //}

        _items = items;

        //When items is List<Object>, can use .sort(Comparison<T>) to sort by individual attributes
        //IComparer<CollectibleObject> comparer = CollectibleObject.name;
        //_items.Sort();
    }

    private void AddItem(CollectibleObject item)
    {
        items.Add(item);
    }

    private void Remove(CollectibleObject item)
    {
        Vector3 playerLoc = transform.position;
        Vector3 playerFor = transform.forward;

        Vector3 itemLoc = playerLoc + playerFor * 2;
        itemLoc.y = 1;

        Quaternion playerRot = transform.rotation;
        Quaternion itemRot = playerRot * Quaternion.Euler(0, 180, 0);

        GameObject newItem = Instantiate(item.gameObject, itemLoc, itemRot, collectiblesTransform);
        newItem.SetActive(true);

        items.Remove(item);
        Destroy(item.gameObject);
    }

    public void Remove()
    {
        GameplayState testState = new GameplayState();
        if(gameManager.currentState == testState && _items.Count > 0 && transform.position.y < 1)
        {
            CollectibleObject item = items[0];

            Remove(item);
        }
    }

    public void Remove(int buttonNum)
    {
        if(buttonNum < _items.Count)
        {
            Remove(items[buttonNum]);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        CollectibleObject collisionItem = hit.gameObject.GetComponent<CollectibleObject>();

        if (collisionItem != null)
        {
            items.Add(collisionItem);
            collisionItem.gameObject.SetActive(false);
        }
    }

}
