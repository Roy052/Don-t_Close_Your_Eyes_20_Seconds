# Don't Close Your Eyes for 20 Seconds
<div>
    <h2> 게임 정보 </h2>
    <img src = "https://img.itch.zone/aW1nLzEwNTk3Mzg2LnBuZw==/347x500/gP8twm.png"><br>
    <img src="https://img.shields.io/badge/Unity-yellow?style=flat-square&logo=Unity&logoColor=FFFFFF"/>
    <img src="https://img.shields.io/badge/Clicker-gray"/>
    <h4> 개발 일자 : 2022.11 <br><br>
    플레이 : https://goodstarter.itch.io/dont-close-your-eyes-for-20-seconds
    
  </div>
  <div>
    <h2> 게임 설명 </h2>
    <h3> 스토리 </h3>
     20초 동안 눈을 떠라<br><br>
    <h3> 게임 플레이 </h3>
     이 게임은 SPACE 키를 연타하며 진행되는 클리커 게임으로 <br><br>
     20초 동안 눈이 감기지 않도록 버티는 게임이다.<br><br>
      <h3> 추가 내용 </h3>
      '20 Second Game Jam' 출품작이다.
  </div> 
  <div>
    <h2> 게임 스크린샷 </h2>
      <table>
        <td><img src = "https://img.itch.zone/aW1hZ2UvMTgwNDU1MC8xMDY2ODIyMy5wbmc=/347x500/017XmA.png"></td>
        <td><img src = "https://img.itch.zone/aW1hZ2UvMTgwNDU1MC8xMDY2ODIyNC5wbmc=/347x500/Plhyps.png"></td>
        <td><img src = "https://img.itch.zone/aW1hZ2UvMTgwNDU1MC8xMDY2ODIyNS5wbmc=/347x500/Ntvjsb.png"></td>
      </table>
  </div>
    <div>
    <h2> 게임 플레이 영상 </h2>
    https://youtu.be/P5pWeMf9Fr0
  </div>
  <div>
    <h2> 배운 점 </h2>
      유니티 내장 함수를 이용해 다른 폴더 안의 Sprite를 불러오고 출력해보았다.<br><br>
      
  </div>
  <div>
    <h2> 수정할 점 </h2>
      폭탄 터지는 시간과 게임이 끝나는 시간 간의 오차.<br><br>
      추가적인 컨텐츠
   <h2> Design Picture </h2>
   <table>
        <td><img src = "https://postfiles.pstatic.net/MjAyMjEyMDJfMTcz/MDAxNjY5OTQ1MTYyNTcz.xRGtDzHsxcJYazZlDcthq5OryoHRCOAIo3IhGdm3-4sg.GZeppaeShzgz5M3EIWUjWJXTdv0lI3WDgx6GlKBlis8g.JPEG.tdj04131/KakaoTalk_20221202_103401185.jpg?type=w773" height = 500></td>
        <td><img src = "https://postfiles.pstatic.net/MjAyMjEyMDJfNDYg/MDAxNjY5OTQ1MTYyNTU0.1gzPKWdthy-1HV3kGPMn-xFlpEmNQUljsOlQcorqdpwg.WTibCAZObK__76rH2hzr5SLjkZHd9qkVYY0WdQ_0MQ4g.JPEG.tdj04131/KakaoTalk_20221202_103401185_01.jpg?type=w773" height = 500></td>
      </table>
  </div>
   <div>
       <h2> 주요 코드 </h2>
       <h4> MainSM SetUp 함수 </h4>
    </div>
    
```csharp
situationNum = Random.Range(0, 4);

mainSprite = Resources.Load<Sprite>("Arts/MainImage/" + situationNum);
midSprite = Resources.Load<Sprite>("Arts/MiddleImage/" + situationNum);

screenImage.GetComponent<SpriteRenderer>().sprite = mainSprite;
```
<div>
    <h4> Eyelid(눈꺼풀) 클래스 </h4>
</div>      
    
```csharp
public class Eyelid : MonoBehaviour{
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
```
