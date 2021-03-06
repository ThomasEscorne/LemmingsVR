﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class lemming_spawner_behaviour : MonoBehaviour
{

    public GameObject lemming;
    public GameObject lemming_object;
    public int axis = 0;
    public Tilemap TilemapForLemming;
    
    
    public int number_of_lemmings;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (number_of_lemmings != 0)
        {
            GameObject container = Instantiate(lemming, new Vector3(transform.position.x, transform.position.y - 0.14f, transform.position.z), Quaternion.identity) as GameObject;
            container.transform.GetChild(0).gameObject.GetComponent<lemming_behavior>().Tilemap = TilemapForLemming;
            if (axis == 1)
                container.transform.Rotate(0, 90, 0);
            else if (axis == 2)
                container.transform.Rotate(0, 180, 0);
            if (lemming_object)
            {
                GameObject stuff = Instantiate(lemming_object) as GameObject;
                GameObject lemming = container.transform.GetChild(0).gameObject;
                lemming.GetComponent<lemming_behavior>().give_object(stuff.GetComponent<LemmingObject>());
            }
            number_of_lemmings--;
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
