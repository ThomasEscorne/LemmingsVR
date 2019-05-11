using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lemming_special_behaviour : MonoBehaviour
{
    public float power = 6f;
    lemming_behavior lemming;
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        lemming = GetComponent<lemming_behavior>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ice") || col.gameObject.CompareTag("Jumper") || col.gameObject.CompareTag("Destructive"))
        {
            lemming.touching_ground--;
            if (lemming.touching_ground <= 0)
            {
                lemming.is_grounded = false;
                lemming.old_y = transform.position.y;
                lemming.StartCoroutine("DieFromFall");
            }
            if (col.gameObject.CompareTag("Destructive"))
                col.gameObject.GetComponent<Destructive_block_behaviour>().addPerson();

        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Destructive") || col.gameObject.CompareTag("Ice"))
        {
            lemming.touching_ground++;
            lemming.is_grounded = true;
            lemming.StopCoroutine("DieFromFall");
            if (transform.position.y < lemming.old_y - 5)
            {
                if (lemming.current_object && lemming.current_object._name == "umbrella")
                    lemming.loose_object();
                else
                {
                    lemming.set_attitude(lemming_behavior.Attitude.DYING);
                    lemming.has_to_die = true;
                }
            }
            lemming.old_y = transform.position.y;
            if (col.gameObject.CompareTag("Destructive"))
                col.gameObject.GetComponent<Destructive_block_behaviour>().substractPerson();
        }
        else if (col.gameObject.CompareTag("Jumper"))
        {
            lemming.touching_ground++;
            Jumper_Behaviour jb = col.gameObject.GetComponent<Jumper_Behaviour>();
            lemming.is_grounded = true;
            lemming.StopCoroutine("DieFromFall");
            rigidBody.AddForce(new Vector3(jb.forceX, jb.forceY) * power);
        }
        else if (col.gameObject.CompareTag("Fire"))
        {
            lemming.is_grounded = false;
            lemming.StopCoroutine("DieFromFall");
            lemming.has_to_die = true;
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Ice"))
        {
            if (lemming.speed < 0.15)
                lemming.speed += 0.001f;
        }
        else if (lemming.speed > 0.025)
        {
            lemming.speed -= 0.003f;
            if (lemming.speed < 0.025f)
                lemming.speed = 0.025f;
        }

    }
    
}
