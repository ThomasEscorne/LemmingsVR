using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public GameObject LevelMenu;
    public GameObject Level1;

    public void StartLevel1()
    {
        StartCoroutine(StartLevel(Level1));
        
    }

    IEnumerator StartLevel(GameObject Level)
    {
        yield return new WaitForSeconds(0.5f);
        LevelMenu.SetActive(false);
        Level.SetActive(true);
    }
}
