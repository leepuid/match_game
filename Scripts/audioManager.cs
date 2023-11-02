using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioSource bgmusicS;
    public AudioClip bgmusicClip;
    // Start is called before the first frame update
    void Start()
    {
        bgmusicS.clip = bgmusicClip;
        bgmusicS.Play();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
