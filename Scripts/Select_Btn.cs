using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_Btn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Select_EZ()
    {

        gameManager.level = 1;
        SceneManager.LoadScene("MainScene");
    }

    void Select_Normal()
    {
        gameManager.level = 2;
        SceneManager.LoadScene("MainScene");
    }

    void Select_Hard()
    {
        gameManager.level = 3;
        SceneManager.LoadScene("MainScene");
    }
}
