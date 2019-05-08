using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : LemmingObject
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(-200, -200, 2000), transform.rotation);
        y_offset = 0.42f;
        _name = "Box";
    }

    protected override void act_on_lemming()
    {
        if (lemming)
        {
            //lemming.GetComponent<Transform>().Rotate(0, 180, 0);
            lemming.GetComponent<lemming_behavior>().IsStill = true;
            lemming.GetComponent<lemming_behavior>().IsADispenser = true;

        }
    }
}
