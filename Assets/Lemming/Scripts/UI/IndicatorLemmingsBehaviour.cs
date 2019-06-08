using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLemmingsBehaviour : MonoBehaviour
{
    private int nbDead = 10000;
    private int nbSaved = 10000;
    private int nbToSpawn = 10000;
    private int nbSpawned = 10000;
    public TextMesh deathText;
    public TextMesh saveText;
    public TextMesh toSpawnText;
    public TextMesh spawnText;

    public void Start()
    {
        nbSaved = nbDead = nbToSpawn = nbSpawned = 0;
    }

    public void setDeath(int nb)
    {
        if (nbDead == nb)
            return;
        nbDead = nb;
        deathText.text = nb.ToString();
    }

    public void setSave(int nb)
    {
        if (nbSaved == nb)
            return;
        nbSaved = nb;
        saveText.text = nb.ToString();
    }

    public void setToSpawn(int nb)
    {
        if (nbToSpawn == nb)
            return;
        nbToSpawn = nb;
        toSpawnText.text = nb.ToString();
    }

    public void setSpawned(int nb)
    {
        if (nbSpawned == nb)
            return;
        nbSpawned = nb;
        spawnText.text = nb.ToString();
    }
}
