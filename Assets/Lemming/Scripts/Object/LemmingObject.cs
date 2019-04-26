using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingObject : MonoBehaviour
{
    protected float x_offset = 0;
    protected float y_offset = 0;
    protected float z_offset = 0;
    public string _name = "basic";
    protected GameObject lemming = null;
    public GameObject BoxOf = null;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(-200, -200, 2000), transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        if (lemming)
            transform.SetPositionAndRotation(new Vector3(lemming.transform.position.x + (x_offset * lemming.GetComponent<lemming_behavior>().direction), lemming.transform.position.y + y_offset, lemming.transform.position.z + z_offset), transform.rotation);
        else
            Destroy(gameObject);
    }

    public  void SetLemming(GameObject _lemming)
    {
        lemming = _lemming;
        act_on_lemming();
    }

    public virtual void loose_object()
    {
        Destroy(gameObject);
    }

    protected virtual void act_on_lemming() { }

    public virtual void GiveOne(lemming_behavior current_lemming)
    {
        GameObject stuff = Instantiate(BoxOf) as GameObject;
        current_lemming.give_object(stuff.GetComponent<LemmingObject>());
    }
}
