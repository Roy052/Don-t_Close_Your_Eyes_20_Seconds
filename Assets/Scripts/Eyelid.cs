using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyelid : MonoBehaviour
{
    [SerializeField] MainSM mainSM;

    float startPos, endPos;
    public float speed = 0;
    public bool startMove = false;
    public bool gameEnd;
   
    void Start()
    {
        startPos = this.transform.position.y;
        Debug.Log(this.name + " : " + startPos);
        if (startPos > 0) endPos = 5.4f;
        else endPos = -5.4f;
        speed = 0.3f;
        gameEnd = false;
    }

    void Update()
    {
        if(startMove)
            this.transform.position += (endPos - startPos) * Time.deltaTime * speed * new Vector3(0,1,0);

        if (startMove == true && (endPos > 0 && endPos - this.transform.position.y > 0) 
            || (endPos < 0 && endPos - this.transform.position.y < 0))
        {
            speed = 0;
            if(mainSM != null && gameEnd == false)
            {
                gameEnd = true;
                StartCoroutine(mainSM.GameOver());
            }
               
        }
    }

    public IEnumerator PositionReset(float time, float waitingTime)
    {
        startMove = false;
        Vector3 currentPos = this.transform.position;

        float tempTime = 0;

        Debug.Log(this.name + " = ( current : " + currentPos + " ), ( start : " + startPos + " )");
        yield return new WaitForSeconds(waitingTime / 2);
        while(tempTime <= time)
        {
            this.transform.position += new Vector3(0, startPos - currentPos.y, 0) * Time.deltaTime / time;
            tempTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForSeconds(waitingTime/2);
        startMove = true;
    }
}
