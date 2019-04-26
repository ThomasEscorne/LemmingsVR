using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : LemmingObject
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(-200, -200, 2000), transform.rotation);
        x_offset = -0.244f;
        y_offset = 0.5f;
        _name = "jetpack";
    }

    protected override void act_on_lemming()
    {
        if (lemming)
        {
            lemming.GetComponent<ConstantForce>().force = new Vector3(0.3f * lemming.GetComponent<lemming_behavior>().direction, +11.2f, 0);
            StartCoroutine("Duration");
        }
    }

    public override void loose_object()
    {
        if (lemming)
        {
            lemming.GetComponent<ConstantForce>().force = new Vector3(0, 0, 0);
            lemming.GetComponent<lemming_behavior>().set_attitude(lemming_behavior.Attitude.FALLING);
        }
        Destroy(gameObject);
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(4);
        if (lemming)
        {
            lemming.GetComponent<lemming_behavior>().loose_object();
        }
    }

}

