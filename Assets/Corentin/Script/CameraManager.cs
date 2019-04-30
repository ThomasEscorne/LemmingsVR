using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float speedRotation;

    public float speedLvl;

    private float rotateY;

    private Vector3 basicPos;

    private Vector3 lvlOnePos;

    private bool lvlOneCam = false;

    private float startTime;

    private float distCam;

    private Vector3 distEndTmp;

    private Vector3 distStartTmp;


    // Start is called before the first frame update
    void Start()
    {
        basicPos = new Vector3(0f, 4f, 0f);
        lvlOnePos = new Vector3(-1.24f, 4.5f, 3.3f);

       // distCamLvlOne = Vector3.Distance(basicPos, lvlOnePos);


    }

    void InitDistanceCamera(Vector3 startMarker, Vector3 endMarker)
    {
        distEndTmp = endMarker;
        distStartTmp = startMarker;
        distCam = Vector3.Distance(startMarker, endMarker);

    }


    // Update is called once per frame
    void Update()
    {
        if (lvlOneCam == true) {

            if (transform.position == distEndTmp)
            {
                lvlOneCam = false;
                return;
            }
            float distCovered = (Time.time - startTime) * speedLvl;

            float coeffDist = distCovered / distCam;

            transform.position = Vector3.Lerp(distStartTmp, distEndTmp, coeffDist);


        }

        if (Input.GetKey(KeyCode.L))
        {
            startTime = Time.time;

            InitDistanceCamera(transform.position, lvlOnePos);
            lvlOneCam = true;

            transform.rotation = Quaternion.identity;

        }

        if (Input.GetKey(KeyCode.Escape))
        {
            startTime = Time.time;

            InitDistanceCamera(transform.position, basicPos);
            lvlOneCam = true;

            transform.rotation = Quaternion.identity;

        }


        rotateY = 0;
        if (!lvlOneCam && transform.position != lvlOnePos)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotateY = -speedRotation;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rotateY = speedRotation;
            }

            transform.Rotate(0, rotateY, 0);
        }
    }
}
