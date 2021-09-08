using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bicycle_movement : MonoBehaviour
{
    /// <summary>
    /// 속도 표기할때 사용
    /// </summary>
    [SerializeField]
    TextMeshProUGUI speed_text;
    /// <summary>
    /// 이동속도
    /// </summary>
    public float moveSpeed = 0f;

    /// <summary>
    /// 최대 가속도
    /// </summary>
    float maxspeed = 55f;
    
    /// <summary>
    /// 앞으로 가면서 왼쪽으로 꺿을 시 지속시간 계산
    /// </summary>
    float time_left = 0;

    /// <summary>
    /// 앞으로 가면서 오른쪽으로 꺿을 시 지속시간 계산
    /// </summary>
    float time_right = 0;

    /// <summary>
    /// 자전거가 땅에 붙어 있는지 체크
    /// </summary>
    bool isGround = true;

    private void FixedUpdate()
    {
        TextManager(); 
        if (isGround)
        {
            Bicycle_Move();
        }
    }

    /// <summary>
    /// y축을 제외한 나머지 축을 0으로 초기화 하면서, 값을 수정
    /// </summary>
    void Rollback()
    {
        // 시점 다시 맞추기
        Debug.Log("y : " + transform.rotation.y);

        transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y, 0);
        
    }

    /// <summary>
    /// 텍스트가 사용되는 부분들을 담당
    /// </summary>
    private void TextManager() {
        speed_text.text = (moveSpeed * 2).ToString("N1");
    }

    /// <summary>
    /// 바이크의 전체적인 이동을 관리하는 메소드
    /// 간단한 앞뒤, 양옆, 그리고 롤백을 담당한다.
    /// </summary>
    void Bicycle_Move()
    {
        // 제자리
        if (Input.GetKeyDown(KeyCode.R))
        {
            Rollback();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //좌측 회전
            transform.Rotate(0, -1.5f, 0);

        }
       else if (Input.GetKey(KeyCode.RightArrow))
        {
            //우측 회전
            transform.Rotate(0, 1.5f, 0);

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //가속도
            if (moveSpeed < maxspeed)
            {
                moveSpeed += Time.deltaTime + 0.2f;
            }
            Tip_Bike();

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Turn(ref time_left, "left");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Turn(ref time_right, "right");
            }
            else
            {
                if(time_left > 0)
                time_left -= Time.deltaTime;
                if(time_right > 0)
                time_right -= Time.deltaTime;
            }         
        }
        else
        {
            //이동속도가 1보다 크면 이동속도 비례해서 감속, 1보다 작으면 0으로 취급
            moveSpeed -= moveSpeed > 1 ? (moveSpeed / 100) : 0;

            time_right = 0;
            time_left = 0;
        }

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow) && moveSpeed > -10)
        {
            // moveSpeed = moveSpeed > 0 ? moveSpeed -= (moveSpeed / 10) : moveSpeed > -10 ? -Time.deltaTime * 8 : moveSpeed = -10;

            //무브 스피드가 0보다 클땐 이동속도에 비례해서 감속하고, 0보다 작을경우 최대 -10까지만 이동
            if (moveSpeed > 1) {
                moveSpeed -= moveSpeed / 100;
            }
            else {
                moveSpeed -= Time.deltaTime * 8;
            }
            //뒤로 이동
            Tip_Bike();
        }
    }
    /// <summary>
    /// 앞으로 가거나 뒤로 가는 상황에서, 잃어버린 중심을 다시 찾기 위해 넣은 코드.
    /// 당연히 자연계 한도만큼만 원래 기울기를 되찾는다. (z축)
    /// </summary>
    private void Tip_Bike() {
        // 기울기 보정 2
        if (gameObject.transform.rotation.eulerAngles.z > 185f) {
            transform.Rotate(0, 0, 0.1f);
            Debug.Log("왼쪽으로 움직임 1 : " + gameObject.transform.rotation.eulerAngles.z);
            if (360f - gameObject.transform.rotation.eulerAngles.z >= 10f && 360f - gameObject.transform.rotation.eulerAngles.z < 60f) {//2중 보정치
                Debug.Log("왼쪽으로 움직임 2 : " + gameObject.transform.rotation.eulerAngles.z);
                transform.Rotate(0, 0, 0.5f);
            }
            else if (360f - gameObject.transform.rotation.eulerAngles.z >= 60f && 360f - gameObject.transform.rotation.eulerAngles.z <= 90f) {
                transform.Rotate(0, 0, 1f);
            }
            Debug.Log("오른쪽으로 기울었다");
        }
        else if (gameObject.transform.rotation.eulerAngles.z > 5f) {
            transform.Rotate(0, 0, -0.1f);
            Debug.Log("오른쪽으로 움직임 1 : " + gameObject.transform.rotation.eulerAngles.z);
            //2중 보정치
            if (gameObject.transform.rotation.eulerAngles.z > 10f && transform.rotation.z < 60f) {
                Debug.Log("오른쪽으로 움직임 2 : " + gameObject.transform.rotation.eulerAngles.z);
                transform.Rotate(0, 0, -0.5f);
            }
            else if (gameObject.transform.rotation.z >= 60f && gameObject.transform.rotation.z <= 90f) {
                transform.Rotate(0, 0, -1f);
            }
            Debug.Log("왼쪽으로 기울었다");
        }
    }
    /// <summary>
    /// 달리는 중에 특정 방향으로 회전
    /// </summary>
    /// <param name="time">특정방향으로 회전을 유지하는 시간을 계산</param>
    /// <param name="direction"></param>
    private void Turn(ref float time, string direction) {
        moveSpeed -= moveSpeed / 200 + time * moveSpeed / 200;
        
        Debug.Log(time * moveSpeed / 100 + "만큼 감속");
        float buho = direction == "right" ? 1 : -1;

        time += Time.deltaTime;
        Debug.Log(direction + "로 기울기 지속시간 : " + time);
        transform.Rotate(0, 0, buho * -time / 2 + buho * -0.1f);
        transform.Rotate(0, (time < 0.5f ? buho * time : buho * time / 2 + buho * 0.3f + 0.1f * buho), 0);
    }

    /// <summary>
    /// 콜라이더 판별
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}

