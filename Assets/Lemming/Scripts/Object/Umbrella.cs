using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : LemmingObject
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(-200, -200, 2000), transform.rotation);
        x_offset = 0.01688f;
        y_offset = 0.42f;
        _name = "umbrella";
    }

    protected override void act_on_lemming() {
        if (lemming)
        {
            lemming.GetComponent<ConstantForce>().force = new Vector3(0, +1.9f, 0);
            lemming.GetComponent<lemming_behavior>().speed = 0.008f; 
        }
    }

    public override void loose_object()
    {
       if (lemming)
        {
            lemming.GetComponent<ConstantForce>().force = new Vector3(0, +1.6f, 0);
            lemming.GetComponent<lemming_behavior>().speed = 0.007f;
        }
        Destroy(gameObject);
    }

}
