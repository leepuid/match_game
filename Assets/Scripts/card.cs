using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public AudioSource cardS;
    public AudioClip cardClip;
    public SpriteRenderer colorC;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void open()
    {
        cardS.PlayOneShot(cardClip);

        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);

        if(gameManager.I.firstPick == null)
        {
            gameManager.I.firstPick = gameObject;
        }
        else
        {
            gameManager.I.secondPick = gameObject;
            gameManager.I.isMatched();
        }
        colorC.material.color = Color.gray;
    }

    public void destroyPick()
    {
        Invoke("destoryPickInvoke", 0.5f);
    }

    void destoryPickInvoke()
    {
        Destroy(gameObject);
    }
    public void closePick()
    {
        Invoke("closePickInvoke", 0.5f);
    }
    void closePickInvoke()
    {
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
    }
}
