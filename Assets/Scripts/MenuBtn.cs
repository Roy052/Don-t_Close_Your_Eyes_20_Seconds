using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBtn : MonoBehaviour
{
    [SerializeField] MenuSM menuSM;
    [SerializeField] int type;
    [SerializeField] Sprite onSprite, offSprite;
    public bool clicked = false;

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
