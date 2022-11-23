using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSM : MonoBehaviour
{
    [SerializeField] GameObject[] eyelids;
    [SerializeField] Text timeText;
    float time = 0, oneSecond = 0;
    bool onetime10sec = false, gameEnd = false;
    int spaceTime = 0;

    public int situationNum = 0;
    bool setupEnd = false;
    [SerializeField] GameObject screenImage;
    Sprite mainSprite, midSprite;

    GameManager gm;

    //peak time
    float[] peakTimes = new float[2] { 999, 999 };
    int count = 0;
    float lastPeakTime = 999;
    bool lastPeakTimeOn = false;
    float eyelidPushForce = 0.5f;
    void Start()
    {
        StartCoroutine(SetUp());
    }

    void Update()
    {
        if (setupEnd)
        {
            time += Time.deltaTime;
            oneSecond += Time.deltaTime;
            timeText.text = time.ToString("0.00");
           
        }
        
        //Peak Times
        if(count < peakTimes.Length && time >= peakTimes[count])
        {
            count++;
            StartCoroutine(PeakTime());
        }

        if(time >= lastPeakTime && lastPeakTimeOn == false)
        {
            lastPeakTimeOn = true;
            eyelids[0].GetComponent<Eyelid>().speed = 0.5f;
            eyelids[1].GetComponent<Eyelid>().speed = 0.5f;
            eyelidPushForce = 0.3f;
        }
        
        if(time >= 1 && time <= 1.001f)
        {
            eyelids[0].GetComponent<Eyelid>().startMove = true;
            eyelids[1].GetComponent<Eyelid>().startMove = true;
        }

        if(time > 11 && onetime10sec == false)
        {
            StartCoroutine(eyelids[0].GetComponent<Eyelid>().PositionReset(0.2f,1));
            StartCoroutine(eyelids[1].GetComponent<Eyelid>().PositionReset(0.2f,1));
            screenImage.GetComponent<SpriteRenderer>().sprite = midSprite;
            gm.AudioON(1, situationNum);
            onetime10sec = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && setupEnd && spaceTime < 15 && gameEnd == false)
        {
            spaceTime++;
            eyelids[0].transform.position += new Vector3(0, eyelidPushForce, 0);
            eyelids[1].transform.position += new Vector3(0, -eyelidPushForce, 0);
        }

        if(oneSecond >= 1)
        {
            spaceTime = 0;
            oneSecond = 0;
        }
        
        if(gameEnd == false && time >= 21)
        {
            StartCoroutine( GameClear());
        }
    }

    public IEnumerator GameOver()
    {
        gameEnd = true;
        eyelids[0].GetComponent<Eyelid>().gameEnd = true;
        eyelids[1].GetComponent<Eyelid>().gameEnd = true;
        gm.AudioON(3, situationNum);
        yield return new WaitForSeconds(2.2f);
        gm.MainToMenu();
    }

    IEnumerator GameClear()
    {
        gameEnd = true;
        eyelids[0].GetComponent<Eyelid>().gameEnd = true;
        eyelids[1].GetComponent<Eyelid>().gameEnd = true;
        gm.AudioON(2, situationNum);
        yield return new WaitForSeconds(2.2f);
        gm.MainToMenu();
    }

    IEnumerator SetUp()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        situationNum = Random.Range(0, 4);

        mainSprite = Resources.Load<Sprite>("Arts/MainImage/" + situationNum);
        midSprite = Resources.Load<Sprite>("Arts/MiddleImage/" + situationNum);

        screenImage.GetComponent<SpriteRenderer>().sprite = mainSprite;

        eyelids[0].transform.position = new Vector3(0, 5.4f, 0);
        eyelids[1].transform.position = new Vector3(0, -5.4f, 0);

        StartCoroutine(eyelids[0].GetComponent<Eyelid>().PositionReset(0.5f, 1));
        StartCoroutine(eyelids[1].GetComponent<Eyelid>().PositionReset(0.5f, 1));

        //PeakTimeSetup
        peakTimes[0] = 5 + Random.Range(0, 0.9f);
        peakTimes[1] = 12.5f + Random.Range(0, 0.9f);
        lastPeakTime = 18;

        yield return new WaitForSeconds(1.5f);
        gm.AudioON(0, situationNum);

        setupEnd = true;
    }

    IEnumerator PeakTime()
    {
        eyelidPushForce = 0.4f;
        eyelids[0].GetComponent<Eyelid>().speed = 0.4f;
        eyelids[1].GetComponent<Eyelid>().speed = 0.4f;
        yield return new WaitForSeconds(1);
        eyelidPushForce = 0.5f;
    }
}
