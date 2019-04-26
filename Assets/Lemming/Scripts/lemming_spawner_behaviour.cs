using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lemming_spawner_behaviour : MonoBehaviour
{

    public GameObject lemming;
    public GameObject lemming_object;

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
        for (int i = 0; i < number_of_lemmings; i++)
        {
            GameObject container = Instantiate(lemming, new Vector3(transform.position.x, transform.position.y - 0.7f, transform.position.z), Quaternion.identity) as GameObject;
            if (lemming_object)
            {
                GameObject stuff = Instantiate(lemming_object) as GameObject;
                GameObject lemming = container.transform.GetChild(0).gameObject;
                lemming.GetComponent<lemming_behavior>().give_object(stuff.GetComponent<LemmingObject>());
            }
           
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
