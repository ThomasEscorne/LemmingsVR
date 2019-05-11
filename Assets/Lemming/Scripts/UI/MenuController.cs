using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LevelMenu;

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

}
