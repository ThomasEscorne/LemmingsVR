using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamControl : MonoBehaviour
{

    [Header("CAMERA")]
    public List<GameObject> Panel;
    public List<Transform> UIpos;
    public enum cam { main, level, option }
    public cam Cam;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void setCam(string newCam)
    {
        if (newCam == "main")
        {
            Cam = cam.main;

            Panel[1].transform.position = UIpos[1].position;
            Panel[0].transform.position = UIpos[0].position;
            Panel[0].SetActive(true);
            StartCoroutine("DisablePanel", Panel[1]);
        }
        else if (newCam == "level")
        {
            Cam = cam.level;

            Panel[0].transform.position = UIpos[1].position;
            Panel[1].transform.position = UIpos[0].position;
            Panel[1].SetActive(true);
            StartCoroutine("DisablePanel", Panel[0]);
        }
    }

    IEnumerator DisablePanel(GameObject panel)
    {
        bool pass = false;

        while (pass != true)
        {
            pass = true;
            yield return new WaitForSeconds(2f);
        }
        panel.SetActive(false);
    }
}
