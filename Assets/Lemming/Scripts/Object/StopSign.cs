using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSign : LemmingObject
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(-200, -200, 2000), transform.rotation);
        _name = "stopSign";
    }


    protected override void act_on_lemming()
    {
        if (lemming)
        {
            lemming.GetComponent<Transform>().Rotate(0, 180, 0);
            lemming.tag = "wall";
            lemming.GetComponent<lemming_behavior>().IsStill = true;
        }
    }
}
