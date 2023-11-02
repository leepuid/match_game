using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager I;
    public Text timeTxt;
    public Animator timeTxtA;
    float time;
    public Text onescore;
    public Text bestScore;
    public Text bestMatch;
    public Text oneMatch;
    public GameObject card;
    public GameObject firstPick;
    public GameObject secondPick;
    public GameObject endpanel;
    public Text matchTxt;
    public AudioSource hurry;
    public AudioClip hurryclip;
    public AudioSource matchS;
    public AudioClip matchClip;
    public AudioSource errS;
    public AudioClip errClip;
    public GameObject minus;
    public Text pickStack;
    int pstack;
    bool isWarring = false;
    public Text levelTxt;
    public static int level;

    // Start is called before the first frame update

    void Awake()
    {
        I = this;   // 싱글톤
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        pstack = 0;
        levelSelect();
        pickSearch();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        

        if (time < 0.0f)
        {
            Invoke("gameEnd", 0.0f);
        }

        if (time < 10.0f && !isWarring)
        {
            hurry.PlayOneShot(hurryclip);
            isWarring = true;
        }

        if (time < 10.0f)
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
            timeTxtA.SetBool("Warring", true);
        }
    }

    public void isMatched()
    {
        string firstPickImage = firstPick.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondPickImage = secondPick.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstPickImage == secondPickImage)
        {
            firstPick.GetComponent<card>().destroyPick();
            secondPick.GetComponent<card>().destroyPick();
            matchS.PlayOneShot(matchClip);

            //매칭에 성공했을 때 이름출력
            matchTxt.text = firstPickImage;

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if (cardsLeft == 2)
            {
                Invoke("gameEnd", 0.0f);
       
            }
        }
        else
        {
            firstPick.GetComponent<card>().closePick();
            secondPick.GetComponent<card>().closePick();
            matchTxt.text = "실패";
            time -= 1.0f;
            minus.SetActive(true);
            errS.PlayOneShot(errClip);
            Invoke("minusback", 1.0f);
            
        }
            firstPick = null;
            secondPick = null;
        pstack += 1;
        pickStack.text = pstack.ToString();
    }
    void gameEnd()
    {
        Time.timeScale = 0f;
        endpanel.SetActive(true);
        levelScore();
        if(time > 0.01f)
        {
            time = 0.0f;
        }
    }

    void minusback()
    {
        minus.SetActive(false);
    }

    void levelSelect()
    {
        if (level == 1)
        {
            time = 40.0f;
            levelTxt.text = "쉬움";
        }
        else if (level == 2)
        {
            time = 35.0f;
            levelTxt.text = "보통";
        }
        else if (level == 3)
        {
            time = 35.0f;
            levelTxt.text = "어려움";
        }
    }

    void levelScore()
    {
        oneMatch.text = pstack.ToString();
        onescore.text = time.ToString("N2");

        if (level == 1)
        {
            if (PlayerPrefs.HasKey("EZbestscore") == false)
            {
                PlayerPrefs.SetFloat("EZbestscore", time);
                PlayerPrefs.SetInt("EZbestmatch", pstack);
            }
            else
            {
                if (PlayerPrefs.GetFloat("EZbestscore") < time)
                {
                    PlayerPrefs.SetFloat("EZbestscore", time);
                    PlayerPrefs.SetInt("EZbestmatch", pstack);
                }
            }
            bestMatch.text = PlayerPrefs.GetInt("EZbestmatch").ToString();
            bestScore.text = PlayerPrefs.GetFloat("EZbestscore").ToString("N2");
        }

        if (level == 2)
        {
            if (PlayerPrefs.HasKey("NRbestscore") == false)
            {
                PlayerPrefs.SetFloat("NRbestscore", time);
                PlayerPrefs.SetInt("NRbestmatch", pstack);
            }
            else
            {
                if (PlayerPrefs.GetFloat("NRbestscore") < time)
                {
                    PlayerPrefs.SetFloat("NRbestscore", time);
                    PlayerPrefs.SetInt("NRbestmatch", pstack);
                }
            }
            bestMatch.text = PlayerPrefs.GetInt("NRbestmatch").ToString();
            bestScore.text = PlayerPrefs.GetFloat("NRbestscore").ToString("N2");
        }

        if (level == 3)
        {
            if (PlayerPrefs.HasKey("HRbestscore") == false)
            {
                PlayerPrefs.SetFloat("HRbestscore", time);
                PlayerPrefs.SetInt("HRbestmatch", pstack);
            }
            else
            {
                if (PlayerPrefs.GetFloat("HRbestscore") < time)
                {
                    PlayerPrefs.SetFloat("HRbestscore", time);
                    PlayerPrefs.SetInt("HRbestmatch", pstack);
                }
            }
            bestMatch.text = PlayerPrefs.GetInt("HRbestmatch").ToString();
            bestScore.text = PlayerPrefs.GetFloat("HRbestscore").ToString("N2");
        }

        //데이터 지우기
        //PlayerPrefs.DeleteAll();
    }

    void pickSearch()
    {
        // 이름으로 찾기
        string[] comps = { "NC", "NC", "Netmarble", "Netmarble", "NEXON", "NEXON", "Riot", "Riot", "Blizzard", "Blizzard", "Activision", "Activision", "UBISOFT", "UBISOFT", "NEOWIZ", "NEOWIZ", "Nimbleneuron", "Nimbleneuron", "neople", "neople" };

        if (level == 1 || level == 2)
        {
            comps = comps.Take(16).OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();    //Take는 배열 중 몇번까지 사용할 것인지, Skip은 몇번까지 스킵하고 사용할것인지
            for (int i = 0; i < 16; i++)
            {
                GameObject NewCard = Instantiate(card);
                NewCard.transform.parent = GameObject.Find("cards").transform;

                float x = (i % 4) * 1.1f - 1.65f;
                float y = (i / 4) * 1.1f - 3.0f;
                NewCard.transform.position = new Vector3(x, y, 0);

                string compName = comps[i];
                NewCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(compName);
            }
        }
        else if (level == 3)
        {
            comps = comps.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();
            for (int i = 0; i < 20; i++)
            {
                GameObject NewCard = Instantiate(card);
                NewCard.transform.parent = GameObject.Find("cards").transform;

                float x = (i % 4) * 1.1f - 1.65f;
                float y = (i / 4) * 1.1f - 4.0f;
                NewCard.transform.position = new Vector3(x, y, 0);

                string compName = comps[i];
                NewCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(compName);
            }
        }
    }
}
