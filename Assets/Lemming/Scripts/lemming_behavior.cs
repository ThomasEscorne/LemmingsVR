using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lemming_behavior : MonoBehaviour
{
    [SerializeField] private string GROUND = "ground";
    [SerializeField] private string WALL = "wall";
    [SerializeField] private string LEMMING = "lemming";
    public float speed = 0.007f;
    public bool is_grounded = false;
    private bool is_climbing = false;
    private bool has_to_turn = false;
    public int touching_ground = 0;
    public bool has_to_die = false;
    public float old_y = 0;
    private bool first_loop = true;
    private bool is_finishing = false;
    public LemmingObject current_object = null;
    public bool IsADispenser = false;
    public bool IsStill = false;
    public int direction = 1;

    
    //Jobs
    public lemming_builder_job BuilderJob;
    
    public lemming_mining_job MiningJob;
        
    public int nb_floors = 3;
    
    public bool is_mining = false;
    
    private bool mining_started = false;
    
    public bool is_building = false;
        
    private bool building_started = false;

    private bool standing_on_built_floor = false;
    
    public enum Attitude
    {
        FALLING,
        FLOATING,
        FLYING,
        WALKING,
        STANDING,
        BUILDING,
        MINING,
        DYING,
        IDLE
    }

    Rigidbody rb;
    Animator anim;
    CapsuleCollider caps_col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        caps_col = GetComponent<CapsuleCollider>();
        old_y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (has_to_die == true)
        {
        
            StartCoroutine("Die");
        }
        else if (is_building)
        {
            set_attitude(Attitude.BUILDING);
            if (!building_started)
                StartCoroutine(this.Building());
        }
        else if (is_mining)
        {
            set_attitude(Attitude.MINING);
            if (!mining_started)
                StartCoroutine(Mining());
        }

        else if (current_object && current_object._name == "jetpack")
            set_attitude(Attitude.FLOATING);
        else if (IsADispenser)
            return;
        else if (IsStill && is_grounded == true)
            set_attitude(Attitude.STANDING);
        else if (has_to_turn == true)
        {
            has_to_turn = false;
            direction = direction * -1;
            transform.Rotate(0, 180, 0);
        }
        else if (is_finishing == true)
        {
            transform.Translate(0, 0, 0.002f);
        }
        else if (is_grounded == true)
        {
            set_attitude(Attitude.WALKING);
            transform.Translate(0, 0, speed);
        }
        else if (current_object && current_object._name == "umbrella")
        {
            set_attitude(Attitude.FLOATING);
            transform.Translate(0, 0, 0.001f);
        }
        else
            set_attitude(Attitude.FALLING);
    }

    IEnumerator Mining()
    {
        gameObject.transform.tag = "wall";
        mining_started = true;
        yield return new WaitForSeconds(3);
        MiningJob.DestroyMapRadius();
        mining_started = false;
        is_mining = false;
        gameObject.transform.tag = "lemming";
        //set_attitude(Attitude.WALKING);
//        yield return new WaitForSeconds(1);
//        MiningJob.SetFacingWallTag();
    }
    
    //TODO: How many layers to create
    //TODO: Rotation depending on player
    IEnumerator Building()
    {
        gameObject.transform.tag = "wall";
        building_started = true;
        is_building = true;
        yield return new WaitForSeconds(3);
        BuilderJob.CreateRamp(3);
        is_building = false;
        //i = number of stairs to make
        for (int i = 0; i < nb_floors - 1; i++)
        {
            set_attitude(Attitude.WALKING);
            //Time to wait until lemming is on a built floor
            yield return new WaitUntil(() => standing_on_built_floor == true);
            is_building = true;
            yield return new WaitForSeconds(3);
            //Time to build the stairs
            BuilderJob.CreateRamp(3);
            is_building = false;
            standing_on_built_floor = false;
        }
        set_attitude(Attitude.WALKING);
        is_building = false;
        building_started = false;
        gameObject.transform.tag = "lemming";
    }

    public void FinishTriggered()
    {
        StartCoroutine("FinishLevel");
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "uncolidable")
        {
            Physics.IgnoreCollision(col.collider, caps_col);
            return;
        }
        if (col.gameObject.tag == "lemming")
        {
            if (IsADispenser)
                current_object.GiveOne(col.gameObject.GetComponent<lemming_behavior>());
            if(!gameObject.CompareTag("wall"))
                Physics.IgnoreCollision(col.collider, caps_col);
        }
        else if (col.gameObject.CompareTag("ground"))
        {
            if (col.gameObject.transform.name == "built_floor")
            {
                standing_on_built_floor = true;
                col.gameObject.transform.name = "old_built_floor";
            }
            touching_ground++;
            is_grounded = true;
            StopCoroutine("DieFromFall");
            if (transform.position.y < old_y - 1)
            {
                if (current_object && current_object._name == "umbrella")
                    loose_object();
                else
                {
                    set_attitude(Attitude.DYING);
                    has_to_die = true;
                }
            }
            old_y = transform.position.y;
        }
        else if (col.gameObject.CompareTag("wall"))
        {
            has_to_turn = true;
        }

    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("ground"))
        {
            touching_ground--;
            if (touching_ground <= 0)
            {
                is_grounded = false;
                old_y = transform.position.y;
                StartCoroutine("DieFromFall");
            }
        }
    }

    IEnumerator Die()
    {
        set_attitude(Attitude.DYING);
        //gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(5);
        OnDestroy();
        Destroy(gameObject);
    }

    IEnumerator DieFromFall()
    {
        yield return new WaitForSeconds(10);
        OnDestroy();
        Destroy(gameObject);
    }

    IEnumerator FinishLevel()
    {
        is_finishing = true;
        if (is_grounded == true)
        {
            anim.SetBool("is_finishing", true);
            yield return new WaitForSeconds(1f);
            OnDestroy();
            Destroy(gameObject);
        }
        else
        {
            yield return new WaitForSeconds(0.15f);
            OnDestroy();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (transform.parent != null) // if object has a parent
            Destroy(transform.parent.gameObject, 0.1f); // destroy parent a few frames later
    }

    public void set_attitude(Attitude attitude)
    {
        if (attitude == Attitude.WALKING)
        {
            anim.SetBool("is_walking", true);
            anim.SetBool("is_falling", false);
            anim.SetBool("is_floating", false);
            anim.SetBool("is_mining", false);
            anim.SetBool("is_building", false);

        }
        else if (attitude == Attitude.FALLING)
        {
            anim.SetBool("is_walking", false);
            anim.SetBool("is_falling", true);
            anim.SetBool("is_floating", false);
        }
        else if (attitude == Attitude.FLOATING)
        {
            anim.SetBool("is_walking", false);
            anim.SetBool("is_floating", true);
        }
        else if (attitude == Attitude.FLYING)
        {
            anim.SetBool("is_walking", false);
            anim.SetBool("is_falling", false);
            anim.SetBool("is_flying", true);
        }
        else if (attitude == Attitude.STANDING)
        {
            anim.SetBool("is_walking", false);
            anim.SetBool("is_falling", false);
            anim.SetBool("is_floating", false);
        }
        else if (attitude == Attitude.BUILDING)
        {
            anim.SetBool("is_walking", false);
            anim.SetBool("is_falling", false);
            anim.SetBool("is_floating", false);
            anim.SetBool("is_building", true);
            anim.SetBool("is_mining", false);
        }
        else if (attitude == Attitude.MINING)
        {
            anim.SetBool("is_walking", false);
            anim.SetBool("is_falling", false);
            anim.SetBool("is_floating", false);
            anim.SetBool("is_building", false);
            anim.SetBool("is_mining", true);
        }
        else if (attitude == Attitude.DYING)
        {
            anim.SetBool("is_dying", true);

        }
    }

    public void give_object(LemmingObject _lemming_object)
    {
        current_object = _lemming_object;
        _lemming_object.SetLemming(gameObject);
    }

    public void loose_object()
    {
        if (current_object)
            current_object.loose_object();
        current_object = null;
    }
}