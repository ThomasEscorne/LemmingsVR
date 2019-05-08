using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lemming_spawner_behaviour : MonoBehaviour
{

    public GameObject lemming;
    public GameObject lemming_object;
    public int axis = 0;

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
            GameObject container = Instantiate(lemming, new Vector3(transform.position.x, transform.position.y - 0.14f, transform.position.z), Quaternion.identity) as GameObject;
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
           
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
