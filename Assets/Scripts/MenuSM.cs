using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSM : MonoBehaviour
{
    bool isChange = false, isStart = false, setupEnd = false;
    float time;

    List<Sprite> screenImages;
    [SerializeField] SpriteRenderer screen;
    [SerializeField] GameObject[] eyelids;


    [SerializeField] GameObject[] menuBtn;

    GameManager gm;

    Coroutine tempCoroutine;
    void Start()
    {
        StartCoroutine(Setup());
    }

    void Update()
    {
        if(setupEnd == true && isStart == false && isChange == false)
            time += Time.deltaTime;

        if(time >= 2)
        {
            tempCoroutine = StartCoroutine(Change());
            time = 0;
        }
    }

    IEnumerator Change()
    {
        isChange = true;
        eyelids[0].GetComponent<Eyelid>().startMove = true;
        eyelids[1].GetComponent<Eyelid>().startMove = true;
        eyelids[0].GetComponent<Eyelid>().speed = 1;
        eyelids[1].GetComponent<Eyelid>().speed = 1;

        yield return new WaitForSeconds(3);
        screen.sprite = screenImages[Random.Range(0, screenImages.Count)];

        StartCoroutine(eyelids[0].GetComponent<Eyelid>().PositionReset(0.5f, 0.5f)); 
        StartCoroutine(eyelids[1].GetComponent<Eyelid>().PositionReset(0.5f, 0.5f));
        yield return new WaitForSeconds(1.2f);

        eyelids[0].GetComponent<Eyelid>().startMove = false;
        eyelids[1].GetComponent<Eyelid>().startMove = false;
        eyelids[0].GetComponent<Eyelid>().speed = 0;
        eyelids[1].GetComponent<Eyelid>().speed = 0;
        isChange = false;
    }

    IEnumerator Setup()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        eyelids[0].transform.position = new Vector3(0, 5.4f, 0);
        eyelids[1].transform.position = new Vector3(0, -5.4f, 0);

        screenImages = new List<Sprite>();
        screenImages.AddRange(Resources.LoadAll<Sprite>("Arts/MainImage/"));
        screen.sprite = screenImages[Random.Range(0, screenImages.Count)];

        yield return new WaitForSeconds(1f);
        StartCoroutine(eyelids[0].GetComponent<Eyelid>().PositionReset(0.5f, 0.5f));
        StartCoroutine(eyelids[1].GetComponent<Eyelid>().PositionReset(0.5f, 0.5f));
        eyelids[0].GetComponent<Eyelid>().speed = 0;
        eyelids[1].GetComponent<Eyelid>().speed = 0;
        yield return new WaitForSeconds(1.2f);
        

        setupEnd = true;
    }

    public IEnumerator GameStart()
    {
        isStart = true;
        if(tempCoroutine != null)
        StopCoroutine(tempCoroutine);

        for (int i = 0; i < 2; i++)
        {
            menuBtn[i].GetComponent<MenuBtn>().clicked = true;
            StartCoroutine(FadeManager.FadeOut(menuBtn[i].GetComponent<SpriteRenderer>(), 0.5f));
            eyelids[i].GetComponent<Eyelid>().startMove = true;
            eyelids[i].GetComponent<Eyelid>().speed = 1;
        }
       
        yield return new WaitForSeconds(2);
        gm.MenuToMain();
    }


    public void GameQuit()
    {
        menuBtn[0].GetComponent<MenuBtn>().clicked = true;
        menuBtn[1].GetComponent<MenuBtn>().clicked = true;
        gm.GameQuit();
        Debug.Log("GameQuit");
    }
}
