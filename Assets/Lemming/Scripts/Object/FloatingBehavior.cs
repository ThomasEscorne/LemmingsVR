using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBehavior : MonoBehaviour
{
    float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.6f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed, 0);
    }
}
