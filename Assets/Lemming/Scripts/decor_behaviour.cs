using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decor_behaviour : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(-1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed);
    }
}
