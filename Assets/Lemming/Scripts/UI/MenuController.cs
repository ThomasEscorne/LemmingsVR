using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance = null;

    public GameObject MainMenu;
    public GameObject LevelMenu;
    public GameObject DefeatPanel;
    public GameObject WinPanel;

    public List<GameObject> levelButton;
    private int lastLevel;

    public void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        lastLevel = 0;
        RefreshLevel();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayButtonPressed()
    {
        Debug.Log("Play button pressed");
        StartCoroutine(MenuTransition(MainMenu, LevelMenu));

    }

    public void BackButtonPressed()
    {
        Debug.Log("Back button pressed");
        StartCoroutine(MenuTransition(LevelMenu, MainMenu));


    }

    IEnumerator MenuTransition(GameObject toDeactivate, GameObject toActivate)
    {
        yield return new WaitForSeconds(0.5f);
        toDeactivate.SetActive(false);
        toActivate.SetActive(true);
    }

    public void PassLevel(int level)
    {
        lastLevel = level > lastLevel ? level : lastLevel;
    }

    public void ReturnSelection()
    {
        GameObject[] levels = GameObject.FindGameObjectsWithTag("Level");
        foreach (var level in levels)
            Destroy(level);
        GameObject[] lemmings = GameObject.FindGameObjectsWithTag("Container");
        foreach (var lemming in lemmings)
            Destroy(lemming);


        WinPanel.SetActive(false);
        DefeatPanel.SetActive(false);
        LevelMenu.SetActive(true);
        RefreshLevel();
    }
    
    private void RefreshLevel()
    {
        int i = 0;
        levelButton.ForEach(delegate (GameObject item)
        {
            if (i < lastLevel)
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
