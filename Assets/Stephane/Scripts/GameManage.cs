using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    [Header("LEVEL")]
    public int lastLevel = 0;
    public List<GameObject> levelButton;
    public GameObject Level;


    [Header("CAMERA")]
    public List<GameObject> Panel;
    public List<Transform> UIpos;
    public enum cam { main, level, option }
    public cam Cam;

    public static GameManage Instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        int i = 0;

        Debug.Log(GameManage.Instance.lastLevel);

        RefreshLevel();

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int level)
    {
        //GameManage.Instance.lastLevel = level > GameManage.Instance.lastLevel ? level : GameManage.Instance.lastLevel;

        //RefreshLevel();
        Level.transform.GetChild(level + 3).gameObject.SetActive(true);
        Panel[1].transform.position = UIpos[1].position;
        StartCoroutine("DisablePanel", Panel[1]);

    }

    public void LevelPassed(int level)
    {
        Level.transform.GetChild(level + 3).gameObject.SetActive(false);

        GameManage.Instance.lastLevel = level > GameManage.Instance.lastLevel ? level : GameManage.Instance.lastLevel;

        Panel[1].transform.position = UIpos[0].position;
        Panel[1].SetActive(true);

        RefreshLevel();
    }

    public void ResetLevel()
    {
        GameManage.Instance.lastLevel = 0;

        RefreshLevel();
    }

    private void RefreshLevel()
    {
        int i = 0;
        levelButton.ForEach(delegate (GameObject item)
        {
            if (i < GameManage.Instance.lastLevel)
            {
                item.transform.GetChild(0).GetComponent<Button>().interactable = true;
                item.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                item.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                item.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                item.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                item.transform.GetChild(0).GetComponent<Button>().interactable = false;
                item.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                item.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                item.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 0.5f);
                item.transform.GetChild(1).gameObject.SetActive(true);
            }
            ++i;
        });
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
