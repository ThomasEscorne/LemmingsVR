using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControler : MonoBehaviour
{
    public List<GameObject> levelButton;


    // Use this for initialization
    void Start()
    {
        int i = 0;

        Debug.Log(GameManage.Instance.lastLevel);
        
        RefreshLevel();
    }

    public void LoadLevel(int level)
    {
        GameManage.Instance.lastLevel = level > GameManage.Instance.lastLevel ? level : GameManage.Instance.lastLevel;

        RefreshLevel();
    }

    public void ResetLevel()
    {
        GameManage.Instance.lastLevel = 0;

        RefreshLevel();
    }

    private void RefreshLevel()
    {
        int i = 0;
        levelButton.ForEach(delegate (GameObject item)
        {
            if (i < GameManage.Instance.lastLevel)
            {
                item.transform.GetChild(0).GetComponent<Button>().interactable = true;
                item.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                item.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                item.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                item.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                item.transform.GetChild(0).GetComponent<Button>().interactable = false;
                item.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                item.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                item.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 0.5f);
                item.transform.GetChild(1).gameObject.SetActive(true);
            }
            ++i;
        });
    }
}
