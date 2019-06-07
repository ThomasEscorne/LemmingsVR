using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRule : MonoBehaviour
{
    portal_end_behaviour[] listPortalEnd;
    lemming_spawner_behaviour[] listPortalStart;
    public GameObject successScreen;
    public GameObject defeatScreen;
    public int nbSafeWin = 1;
    public int nbDeathLoose = 1;
    public int nbToSpawn = 0;
    private bool allSpawned = false;
    public bool isLost;
    public bool isWin;
    private int nbSaved = 0;
    public int currentLvl;
    public LevelHandler lvlHandler;
    delegate void SpawnLevel();
    List<SpawnLevel> list;
    public bool waitNext;

    void Start()
    {
        waitNext = true;
        list = new List<SpawnLevel>();
        list.Add(lvlHandler.StartLevel1);
        list.Add(lvlHandler.StartLevel2);
        list.Add(lvlHandler.StartLevel3);
        list.Add(lvlHandler.StartLevel4);
    }
    
    public void SetSpawner()
    {
        nbToSpawn = 0;
        listPortalStart = FindObjectsOfType<lemming_spawner_behaviour>();
        for (int i = 0; i < listPortalStart.Length; i++)
            nbToSpawn += listPortalStart[i].number_of_lemmings;
        isWin = false;
        isLost = false;
        allSpawned = false;
        waitNext = false;
    }

    public void RetryLevel()
    {
        lemming_behavior[] listLemming;

        listLemming = FindObjectsOfType<lemming_behavior>();
        for (int i = 0; i < listLemming.Length; i++)
            Destroy(listLemming[i].gameObject);
        defeatScreen.SetActive(false);
        list[currentLvl]();
        waitNext = true;
    }

    public void NextLevel()
    {
        lemming_behavior[] listLemming;

        listLemming = FindObjectsOfType<lemming_behavior>();
        for (int i = 0; i < listLemming.Length; i++)
            Destroy(listLemming[i].gameObject);
        successScreen.SetActive(false);
        currentLvl++;
        list[currentLvl]();
        isLost = false;
        isWin = false;
    }

    private void isGameWon()
    {
        nbSaved = 0;

        listPortalEnd = FindObjectsOfType<portal_end_behaviour>();
        for (int i = 0; i < listPortalEnd.Length; i++)
            nbSaved += listPortalEnd[i].lemmings_saved;
        if (nbSaved >= nbSafeWin)
            isWin = true;
        if (isWin)
        {
            successScreen.SetActive(true);
            waitNext = true;
        }
    }

    private void isGameLost(int leftToSpawn)
    {
        int lemmingsDead;
        lemming_behavior[] listLemming;

        listLemming = FindObjectsOfType<lemming_behavior>();
        lemmingsDead = nbToSpawn - (listLemming.Length + leftToSpawn + nbSaved);
        Debug.Log("Left to spawn = " + leftToSpawn + " listLemming.Length = " + listLemming.Length + " nbToSpawn = " + nbToSpawn + " nbSaved = " + nbSaved);
        for (int i = 0; i < listLemming.Length; i++)
            if (listLemming[i].IsStill || listLemming[i].IsADispenser)
                lemmingsDead++;
        if (lemmingsDead >= nbDeathLoose)
            isLost = true;
        if (isLost)
        {
            defeatScreen.SetActive(true);
            waitNext = true;
        }
    }

    private void CheckSpawnerRoutine()
    {
        int leftToSpawn = 0;
        
        listPortalStart = FindObjectsOfType<lemming_spawner_behaviour>();
        for (int i = 0; i < listPortalStart.Length; i++)
            leftToSpawn += listPortalStart[i].number_of_lemmings;
        if (leftToSpawn == 0)
            allSpawned = true;
        isGameLost(leftToSpawn);
    }

    void Update()
    {
        if (waitNext)
            return;
        isGameWon();
        if (!allSpawned)
            CheckSpawnerRoutine();
        else
            isGameLost(0);
    }
}
