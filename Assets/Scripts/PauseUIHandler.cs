using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIHandler : MonoBehaviour
{
    public GameManager manager;
    public GameObject pauseUIPanel;
    public List<GameObject> pauseUIButtons = new List<GameObject>();


    private void OnEnable()
    {
        pauseUIButtons.Clear();
        AddDesendants(pauseUIPanel.transform, pauseUIButtons);
    }

    public void OnPauseUIButton(int buttonNum)
    {
        switch (buttonNum)
        {
            case 0:
                //Resume
                manager.Resume();
                break;
            case 1:
                //exit
                Debug.Log("quit");
                Application.Quit();
                break;
        }
    }

    private void AddDesendants(Transform parent, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == "Button")
            {
                list.Add(child.gameObject);
            }
        }
    }
}
