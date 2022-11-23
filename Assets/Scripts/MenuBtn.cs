using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBtn : MonoBehaviour
{
    [SerializeField] MenuSM menuSM;
    [SerializeField] int type;
    [SerializeField] Sprite onSprite, offSprite;
    public bool clicked = false;
    float time = 0;
    private void Update()
    {
        time += Time.deltaTime;

        if(time >= 0.55f)
        {
            time = 0;
            StartCoroutine(ButtonMoves());
        }
    }

    IEnumerator ButtonMoves()
    {
        Vector3 save = this.transform.position;

        for(int i = 0; i < 25; i++)
        {
            Vector3 temp = this.transform.position;
            temp.x += Random.Range(-0.015f, 0.015f);
            temp.y += Random.Range(-0.015f, 0.015f);
            this.transform.position = temp;
            yield return new WaitForSeconds(0.02f);
        }
        this.transform.position = save;
    }

    private void OnMouseEnter()
    {
        if(clicked == false)
        {
            this.GetComponent<SpriteRenderer>().sprite = onSprite;
        }
    }
    private void OnMouseDown()
    {
        if (clicked == false)
        {
            if (type == 0)
                StartCoroutine(menuSM.GameStart());
            else
                menuSM.GameQuit();
        }
    }
    private void OnMouseExit()
    {
        if (clicked == false)
        {
            this.GetComponent<SpriteRenderer>().sprite = offSprite;
        }


    }

}
