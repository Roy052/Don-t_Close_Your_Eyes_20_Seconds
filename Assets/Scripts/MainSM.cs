using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSM : MonoBehaviour
{
    List<GameObject> objectList;
    [SerializeField] GameObject[] eyelids;
    [SerializeField] Text timeText;
    float time = 0, oneSecond = 0;
    bool onetime10sec = false, gameEnd = false;
    int spaceTime = 0;

    int situationNum = 0;
    void Start()
    {
        objectList = new List<GameObject>();
    }

    void Update()
    {
        time += Time.deltaTime;
        oneSecond += Time.deltaTime;
        timeText.text = time.ToString("0.00");
        if(time >= 1 && time <= 1.001f)
        {
            eyelids[0].GetComponent<Eyelid>().startMove = true;
            eyelids[1].GetComponent<Eyelid>().startMove = true;
        }

        if(time > 10 && onetime10sec == false)
        {
            StartCoroutine(eyelids[0].GetComponent<Eyelid>().PositionReset());
            StartCoroutine(eyelids[1].GetComponent<Eyelid>().PositionReset());
            onetime10sec = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && spaceTime < 15 && gameEnd == false)
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
        
        if(gameEnd == false && time >= 20)
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
}
