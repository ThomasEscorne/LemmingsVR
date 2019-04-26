using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : LemmingObject
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(-200, -200, 2000), transform.rotation);
        x_offset = 0.0844f;
        y_offset = 2.1f;
        _name = "umbrella";
    }

    protected override void act_on_lemming() {
        if (lemming)
        {
            lemming.GetComponent<ConstantForce>().force = new Vector3(0, +8.2f, 0);
        }
    }

    public override void loose_object()
    {
       if (lemming)
        {
            lemming.GetComponent<ConstantForce>().force = new Vector3(0, 0, 0);
        }
        Destroy(gameObject);
    }

}
