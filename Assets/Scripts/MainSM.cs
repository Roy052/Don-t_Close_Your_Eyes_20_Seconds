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

    int situationNum = 0;
    bool setupEnd = false;
    [SerializeField] GameObject screenImage;
    Sprite mainSprite, midSprite;

    GameManager gm;
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
            onetime10sec = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && setupEnd && spaceTime < 15 && gameEnd == false)
        {
            spaceTime++;
            eyelids[0].transform.position += new Vector3(0, 1f, 0);
            eyelids[1].transform.position += new Vector3(0, -1f, 0);
        }

        if(oneSecond >= 1)
        {
            spaceTime = 0;
            oneSecond = 0;
        }
        
        if(gameEnd == false && time >= 21)
        {
            GameClear();
        }
    }

    public void GameOver()
    {
        gameEnd = true;
    }

    public void GameClear()
    {
        gameEnd = true;
    }

    IEnumerator SetUp()
    {
        mainSprite = Resources.Load<Sprite>("Arts/MainImage/" + situationNum);
        midSprite = Resources.Load<Sprite>("Arts/MiddleImage/" + situationNum);

        screenImage.GetComponent<SpriteRenderer>().sprite = mainSprite;

        eyelids[0].transform.position = new Vector3(0, 5.4f, 0);
        eyelids[1].transform.position = new Vector3(0, -5.4f, 0);

        StartCoroutine(eyelids[0].GetComponent<Eyelid>().PositionReset(0.5f, 1));
        StartCoroutine(eyelids[1].GetComponent<Eyelid>().PositionReset(0.5f, 1));
        yield return new WaitForSeconds(1.5f);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        setupEnd = true;
    }
}
