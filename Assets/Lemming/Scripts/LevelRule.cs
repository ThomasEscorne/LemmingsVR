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
    public bool isLost = false;
    public bool isWin = false;
    private int nbSaved = 0;

    void Start()
    {
        listPortalStart = FindObjectsOfType<lemming_spawner_behaviour>();
        for (int i = 0; i < listPortalStart.Length; i++)
            nbToSpawn += listPortalStart[i].number_of_lemmings;
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
            successScreen.SetActive(true);
    }

    private void isGameLost(int leftToSpawn)
    {
        int lemmingsDead;
        lemming_behavior[] listLemming;

        listLemming = FindObjectsOfType<lemming_behavior>();
        lemmingsDead = nbToSpawn - (listLemming.Length + leftToSpawn + nbSaved);
        for (int i = 0; i < listLemming.Length; i++)
            if (listLemming[i].IsStill || listLemming[i].IsADispenser)
                lemmingsDead++;
        if (lemmingsDead >= nbDeathLoose)
            isLost = true;
        if (isLost)
            defeatScreen.SetActive(true);
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
        isGameWon();
        if (!allSpawned)
            CheckSpawnerRoutine();
        else
            isGameLost(0);
    }
}
