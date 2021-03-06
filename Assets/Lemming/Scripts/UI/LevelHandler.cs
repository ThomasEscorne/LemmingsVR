﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    public GameObject LevelMenu;
    public GameObject[] LevelList;
    public GameObject Menu;
    public GameObject Canvas;
    private LevelRule levelRule;
    private GameObject sceneObject;

    

    public void Start()
    {
        levelRule = GameObject.FindGameObjectWithTag("LevelRule").GetComponent<LevelRule>();
        sceneObject = GameObject.FindGameObjectWithTag("SceneObject");
    }

    public void StartLevel1()
    {
        StartCoroutine(StartLevel(LevelList[0]));
        levelRule.nbSafeWin = 5;
        levelRule.nbDeathLoose = 6;
        levelRule.currentLvl = 0;
        levelRule.score = 3400;
    }

    public void StartLevel2()
    {
        StartCoroutine(StartLevel(LevelList[1]));
        levelRule.nbSafeWin = 5;
        levelRule.nbDeathLoose = 6;
        levelRule.currentLvl = 1;
        levelRule.score = 6000;
    }

    public void StartLevel3()
    {
        StartCoroutine(StartLevel(LevelList[2]));
        levelRule.nbSafeWin = 5;
        levelRule.nbDeathLoose = 6;
        levelRule.currentLvl = 2;
        levelRule.score = 7000;
    }


    public void StartLevel4()
    {
        StartCoroutine(StartLevel(LevelList[3]));
        levelRule.nbSafeWin = 5;
        levelRule.nbDeathLoose = 6;
        levelRule.currentLvl = 3;
        levelRule.score = 8000;
    }

    IEnumerator StartLevel(GameObject Level)
    {
        GameObject[] tmpLevel;

        yield return new WaitForSeconds(0.5f);
        if ((tmpLevel = GameObject.FindGameObjectsWithTag("Level")) != null)
            for (int i = 0; i<tmpLevel.Length; i++)
                Destroy(tmpLevel[i]);
        LevelMenu.SetActive(false);
        Canvas.SetActive(false);
        Instantiate(Level, sceneObject.transform);
        levelRule.SetSpawner();
    }
}
