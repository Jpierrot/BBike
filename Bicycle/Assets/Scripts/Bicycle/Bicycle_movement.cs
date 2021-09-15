using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

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
    float maxspeed = 65f;

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

    /// <summary>
    /// ĸ�� ĳ��Ʈ ������ ����
    /// </summary>
    private RaycastHit hitInfo;

    /// <summary>
    /// ĸ�� ĳ��Ʈ���� �ν��ϴ� �÷��̾��� �ݶ��̴�
    /// </summary>
    public CapsuleCollider collider;

    /// <summary>
    /// ĸ�� ĳ��Ʈ���� ����� ray
    /// </summary>
    public Ray ray;


    public Volume volume;
    Bloom bloom;
    MotionBlur motionBlur;
   LensDistortion lensDistortion;

    private void Start() {
      
        collider = GetComponent<CapsuleCollider>();
        volume.profile.TryGet(out motionBlur);
        volume.profile.TryGet(out lensDistortion);
        
    }

    private void FixedUpdate() {
        TextManager();
        Bicycle_Move();
        GroundCheck();
        SpeedEffect();
    }

    public LayerMask layerMask;

    void SpeedEffect() {
        motionBlur.clamp.value = moveSpeed / 100;
        //�ӵ��� 100�� ������, ȭ���� �������� ���� ȿ���� ������ ����
        if (moveSpeed * 2 > 80)
            lensDistortion.intensity.value -= lensDistortion.intensity.value > -0.6 ? Time.fixedDeltaTime / 5 : 0;
        else {
            lensDistortion.intensity.value += lensDistortion.intensity.value < 0.1 ? Time.fixedDeltaTime / 3 : 0;
        }
    }
    /// <summary>
    /// y���� ������ ������ ���� 0���� �ʱ�ȭ �ϸ鼭, ���� ����
    /// </summary>
    void Rollback() {
        int target = GetComponent<PlayerTrack>().nextTarget - 1;
        // ���� �ٽ� ���߱�
        Debug.Log("y : " + transform.rotation.y);
        transform.position = GameManager.instance.target[target >= 0 ? target : 2].position;
        transform.rotation = GameManager.instance.target[target >= 0 ? target : 2].rotation;
        moveSpeed = 1;
    }

    void GroundCheck() 
     {
        ray.direction = -transform.up;
        ray.origin = transform.position;
        if (Physics.CapsuleCast(ray.origin - transform.forward * (collider.height / 2 - collider.radius), ray.origin + transform.forward * 
             (collider.height / 2 - collider.radius), collider.radius, ray.direction,  out hitInfo, collider.radius + 1.1f, layerMask)) {
            Debug.Log("����ĳ��Ʈ ��Ʈ");
            isGround = true;
        }
         else {
            Debug.Log("����ĳ��Ʈ ����");
            
            Debug.Log("distance : " + (collider.radius + 1.1f));
            isGround = false;
        }
     }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }

    /// <summary>
    /// �ؽ�Ʈ�� ���Ǵ� �κе��� ���
    /// </summary>
    private void TextManager() {
        if(moveSpeed < 0.0f)
        {
            speed_text.text = (moveSpeed * -2).ToString("N1");
        }
        else
            speed_text.text = (moveSpeed * 2).ToString("N1");
    }

    /// <summary>
    /// ����ũ�� ��ü���� �̵��� �����ϴ� �޼ҵ�
    /// ������ �յ�, �翷, �׸��� �ѹ��� ����Ѵ�.
    /// </summary>
    void Bicycle_Move() {
        if (isGround == false) {
            moveSpeed -= Time.fixedDeltaTime * (moveSpeed / 8);
        }

        if (gameObject.transform.position.y < -10)
            Rollback();
       else  if (gameObject.transform.rotation.eulerAngles.z > 360)
            Rollback();

        // ���ڸ�
        if (Input.GetKeyDown(KeyCode.R)) {
            Rollback();
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            //���� ȸ��
            transform.Rotate(0, -1.5f, 0);

        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            //���� ȸ��
            transform.Rotate(0, 1.5f, 0);

        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            //���ӵ�
            if (moveSpeed < maxspeed && isGround) {
                moveSpeed += Time.fixedDeltaTime + 0.2f;
            }
            Tip_Bike();

            if (Input.GetKey(KeyCode.LeftArrow)) {
                Turn(ref time_left, "left");
            }
            else if (Input.GetKey(KeyCode.RightArrow)) {
                Turn(ref time_right, "right");
            }
            else {
                if (time_left > 0)
                    time_left -= Time.fixedDeltaTime;
                if (time_right > 0)
                    time_right -= Time.fixedDeltaTime;
            }
        }
        else 
        {
            //�̵��ӵ��� 1���� ũ�� �̵��ӵ� ����ؼ� ����, 1���� ������ 0���� ���
            moveSpeed -= moveSpeed > 1 ? (moveSpeed / 100) : 0;
            time_right = 0;
            time_left = 0;
        }

        transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.DownArrow) && moveSpeed > -10) {
            // moveSpeed = moveSpeed > 0 ? moveSpeed -= (moveSpeed / 10) : moveSpeed > -10 ? -Time.deltaTime * 8 : moveSpeed = -10;

            //���� ���ǵ尡 0���� Ŭ�� �̵��ӵ��� ����ؼ� �����ϰ�, 0���� ������� �ִ� -10������ �̵�
            if (moveSpeed > 1) {
                moveSpeed -= moveSpeed / 100;
            }
            else {
                moveSpeed -= Time.fixedDeltaTime * 8;
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

        time += Time.fixedDeltaTime;
        Debug.Log(direction + "�� ���� ���ӽð� : " + time);
        transform.Rotate(0, 0, buho * -time / 2 + buho * -0.1f);
        transform.Rotate(0, (time < 0.5f ? buho * time : buho * time / 2 + buho * 0.3f + 0.1f * buho), 0);
    }

    

}

