using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyelid : MonoBehaviour
{
    [SerializeField] MainSM mainSM;

    float startPos, endPos;
    public float speed = 0.5f;
    public bool startMove = false;
    
    void Start()
    {
        startPos = this.gameObject.transform.position.y;
        if (startPos > 0) endPos = 5.4f;
        else endPos = -5.4f;
    }

    void Update()
    {
        if(startMove)
            this.transform.position += (endPos - startPos) * Time.deltaTime * speed * new Vector3(0,1,0);

        if ((endPos > 0 && endPos - this.transform.position.y > 0) 
            || (endPos < 0 && endPos - this.transform.position.y < 0))
        {
            speed = 0;
            mainSM.GameOver();
        }
    }

    public IEnumerator PositionReset()
    {
        startMove = false;
        this.transform.position = new Vector3(0, startPos, 0);
        yield return new WaitForSeconds(1);
        startMove = true;
    }
}
