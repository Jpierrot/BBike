using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bicycle_movement : MonoBehaviour
{
    /// <summary>
    /// �ӵ� ǥ���Ҷ� ���
    /// </summary>
    [SerializeField]
    TextMeshProUGUI speed_text;
    /// <summary>
    /// �̵��ӵ�
    /// </summary>
    public float moveSpeed = 0f;

    /// <summary>
    /// �ִ� ���ӵ�
    /// </summary>
    float maxspeed = 55f;
    
    /// <summary>
    /// ������ ���鼭 �������� ���� �� ���ӽð� ���
    /// </summary>
    float time_left = 0;

    /// <summary>
    /// ������ ���鼭 ���������� ���� �� ���ӽð� ���
    /// </summary>
    float time_right = 0;

    /// <summary>
    /// �����Ű� ���� �پ� �ִ��� üũ
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
    /// y���� ������ ������ ���� 0���� �ʱ�ȭ �ϸ鼭, ���� ����
    /// </summary>
    void Rollback()
    {
        // ���� �ٽ� ���߱�
        Debug.Log("y : " + transform.rotation.y);

        transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y, 0);
        
    }

    /// <summary>
    /// �ؽ�Ʈ�� ���Ǵ� �κе��� ���
    /// </summary>
    private void TextManager() {
        speed_text.text = (moveSpeed * 2).ToString("N1");
    }

    /// <summary>
    /// ����ũ�� ��ü���� �̵��� �����ϴ� �޼ҵ�
    /// ������ �յ�, �翷, �׸��� �ѹ��� ����Ѵ�.
    /// </summary>
    void Bicycle_Move()
    {
        // ���ڸ�
        if (Input.GetKeyDown(KeyCode.R))
        {
            Rollback();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //���� ȸ��
            transform.Rotate(0, -1.5f, 0);

        }
       else if (Input.GetKey(KeyCode.RightArrow))
        {
            //���� ȸ��
            transform.Rotate(0, 1.5f, 0);

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //���ӵ�
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
            //�̵��ӵ��� 1���� ũ�� �̵��ӵ� ����ؼ� ����, 1���� ������ 0���� ���
            moveSpeed -= moveSpeed > 1 ? (moveSpeed / 100) : 0;

            time_right = 0;
            time_left = 0;
        }

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow) && moveSpeed > -10)
        {
            // moveSpeed = moveSpeed > 0 ? moveSpeed -= (moveSpeed / 10) : moveSpeed > -10 ? -Time.deltaTime * 8 : moveSpeed = -10;

            //���� ���ǵ尡 0���� Ŭ�� �̵��ӵ��� ����ؼ� �����ϰ�, 0���� ������� �ִ� -10������ �̵�
            if (moveSpeed > 1) {
                moveSpeed -= moveSpeed / 100;
            }
            else {
                moveSpeed -= Time.deltaTime * 8;
            }
            //�ڷ� �̵�
            Tip_Bike();
        }
    }
    /// <summary>
    /// ������ ���ų� �ڷ� ���� ��Ȳ����, �Ҿ���� �߽��� �ٽ� ã�� ���� ���� �ڵ�.
    /// �翬�� �ڿ��� �ѵ���ŭ�� ���� ���⸦ ��ã�´�. (z��)
    /// </summary>
    private void Tip_Bike() {
        // ���� ���� 2
        if (gameObject.transform.rotation.eulerAngles.z > 185f) {
            transform.Rotate(0, 0, 0.1f);
            Debug.Log("�������� ������ 1 : " + gameObject.transform.rotation.eulerAngles.z);
            if (360f - gameObject.transform.rotation.eulerAngles.z >= 10f && 360f - gameObject.transform.rotation.eulerAngles.z < 60f) {//2�� ����ġ
                Debug.Log("�������� ������ 2 : " + gameObject.transform.rotation.eulerAngles.z);
                transform.Rotate(0, 0, 0.5f);
            }
            else if (360f - gameObject.transform.rotation.eulerAngles.z >= 60f && 360f - gameObject.transform.rotation.eulerAngles.z <= 90f) {
                transform.Rotate(0, 0, 1f);
            }
            Debug.Log("���������� ������");
        }
        else if (gameObject.transform.rotation.eulerAngles.z > 5f) {
            transform.Rotate(0, 0, -0.1f);
            Debug.Log("���������� ������ 1 : " + gameObject.transform.rotation.eulerAngles.z);
            //2�� ����ġ
            if (gameObject.transform.rotation.eulerAngles.z > 10f && transform.rotation.z < 60f) {
                Debug.Log("���������� ������ 2 : " + gameObject.transform.rotation.eulerAngles.z);
                transform.Rotate(0, 0, -0.5f);
            }
            else if (gameObject.transform.rotation.z >= 60f && gameObject.transform.rotation.z <= 90f) {
                transform.Rotate(0, 0, -1f);
            }
            Debug.Log("�������� ������");
        }
    }
    /// <summary>
    /// �޸��� �߿� Ư�� �������� ȸ��
    /// </summary>
    /// <param name="time">Ư���������� ȸ���� �����ϴ� �ð��� ���</param>
    /// <param name="direction"></param>
    private void Turn(ref float time, string direction) {
        moveSpeed -= moveSpeed / 200 + time * moveSpeed / 200;
        
        Debug.Log(time * moveSpeed / 100 + "��ŭ ����");
        float buho = direction == "right" ? 1 : -1;

        time += Time.deltaTime;
        Debug.Log(direction + "�� ���� ���ӽð� : " + time);
        transform.Rotate(0, 0, buho * -time / 2 + buho * -0.1f);
        transform.Rotate(0, (time < 0.5f ? buho * time : buho * time / 2 + buho * 0.3f + 0.1f * buho), 0);
    }

    /// <summary>
    /// �ݶ��̴� �Ǻ�
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

