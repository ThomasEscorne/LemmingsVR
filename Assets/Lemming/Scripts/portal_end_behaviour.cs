using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal_end_behaviour : MonoBehaviour
{
    public int lemmings_saved = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "lemming")
        {
            other.gameObject.GetComponent<lemming_behavior>().FinishTriggered();
            lemmings_saved += 1;
        }
    }
}
