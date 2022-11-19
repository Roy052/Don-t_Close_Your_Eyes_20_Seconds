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
    void Start()
    {
        screenImages = new List<Sprite>();
        screenImages.AddRange(Resources.LoadAll<Sprite>("Arts/MainImage/"));
        screen.sprite = screenImages[Random.Range(0, screenImages.Count)];
        eyelids[0].GetComponent<Eyelid>().speed = 1;
        eyelids[1].GetComponent<Eyelid>().speed = 1;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        setupEnd = true;
    }

    void Update()
    {
        if(setupEnd == true && isStart == false && isChange == false)
            time += Time.deltaTime;

        if(time >= 2)
        {
            StartCoroutine(Change());
            time = 0;
        }
    }

    IEnumerator Change()
    {
        isChange = true;
        eyelids[0].GetComponent<Eyelid>().startMove = true;
        eyelids[1].GetComponent<Eyelid>().startMove = true;

        yield return new WaitForSeconds(3);
        screen.sprite = screenImages[Random.Range(0, screenImages.Count)];

        StartCoroutine(eyelids[0].GetComponent<Eyelid>().PositionReset(0.5f, 0.5f)); 
        StartCoroutine(eyelids[1].GetComponent<Eyelid>().PositionReset(0.5f, 0.5f));
        yield return new WaitForSeconds(1.2f);

        eyelids[0].GetComponent<Eyelid>().startMove = false;
        eyelids[1].GetComponent<Eyelid>().startMove = false;
        eyelids[0].GetComponent<Eyelid>().speed = 1;
        eyelids[1].GetComponent<Eyelid>().speed = 1;
        isChange = false;
    }

    public IEnumerator GameStart()
    {
        isStart = true;
        for (int i = 0; i < 2; i++)
        {
            menuBtn[i].GetComponent<MenuBtn>().clicked = true;
            StartCoroutine(FadeManager.FadeOut(menuBtn[i].GetComponent<SpriteRenderer>(), 0.5f));
            eyelids[i].GetComponent<Eyelid>().startMove = true;
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
