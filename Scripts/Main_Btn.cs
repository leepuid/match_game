using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Btn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void toStart()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void toSelect()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
