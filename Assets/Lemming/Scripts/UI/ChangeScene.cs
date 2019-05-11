using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LevelToLoad(int level)
    {
        GameManage.Instance.lastLevel = level > GameManage.Instance.lastLevel ? level : GameManage.Instance.lastLevel;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetLevel()
    {
        GameManage.Instance.lastLevel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
