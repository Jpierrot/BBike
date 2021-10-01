using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Bicycle_movement : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4];
    GameObject[] wheelMesh = new GameObject[2];

    public float power = 100f; // 바퀴를 회전시킬 힘
    public float rot = 45f; // 바퀴의 회전 각도
    Rigidbody rb;
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

    /// <summary>
    /// 캡슐 캐스트 정보를 받음
    /// </summary>
    private RaycastHit hitInfo;

    /// <summary>
    /// 캡슐 캐스트에서 인식하는 플레이어의 콜라이더
    /// </summary>
    public CapsuleCollider collider;

    /// <summary>
    /// 캡슐 캐스트에서 사용할 ray
    /// </summary>
    public Ray ray;

    public bool mb = false;

    public float ld_ = 0.6f;

    public Volume volume;
    MotionBlur motionBlur;
    LensDistortion lensDistortion;

    private void Start() {

        wheelMesh = GameObject.FindGameObjectsWithTag("WheelMesh");
        rb = GetComponent<Rigidbody>();
        // 무게 중심을 y축 아래방향으로 낮춘다.

        moveSpeed += 40;
        collider = GetComponent<CapsuleCollider>();
        volume.profile.TryGet(out motionBlur);
        volume.profile.TryGet(out lensDistortion);

        if (PlayerPrefs.HasKey("MotionBlur")) {
            int mb_temp;
            mb_temp = PlayerPrefs.GetInt("MotionBlur");
            mb = mb_temp == 1 ? true : false;
        }

      if(PlayerPrefs.HasKey(" LensDistortion")) {

            ld_ = PlayerPrefs.GetFloat("LensDistorition");
        }
    }

    private void FixedUpdate() {
        //WheelPosAndAni();
        TextManager();
        //Bicycle_Move();
        Movement_Re();
        GroundCheck();
        SpeedEffect();
    }

    public LayerMask layerMask;

    void WheelPosAndAni() {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++) {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
    }


    void SpeedEffect() {
        if(mb)
            motionBlur.clamp.value = moveSpeed / 100;
        //속도가 100이 넘으면, 화면이 빨려들어가는 듯한 효과를 서서히 넣음
            if (moveSpeed * 2 > 80)
                lensDistortion.intensity.value -= lensDistortion.intensity.value > -ld_ ? Time.fixedDeltaTime / 5 : 0;
            else {
                lensDistortion.intensity.value += lensDistortion.intensity.value < 0.1 ? Time.fixedDeltaTime / 3 : 0;
            }
        
    }

    private void Movement_Re() {
        for (int i = 0; i < wheels.Length; i++) {
            // for문을 통해서 휠콜라이더 전체를 Vertical 입력에 따라서 power만큼의 힘으로 움직이게한다.
            wheels[i].motorTorque = Input.GetAxis("Vertical") * power;
        }
            // 앞바퀴만 각도전환이 되어야하므로 for문을 앞바퀴만 해당되도록 설정한다.
            wheels[0].steerAngle = Input.GetAxis("Horizontal") * rot;
        wheels[1].steerAngle = Input.GetAxis("Horizontal") * rot;
    }
    /// <summary>
    /// y축을 제외한 나머지 축을 0으로 초기화 하면서, 값을 수정
    /// </summary>
    void Rollback() {
        int target = GetComponent<PlayerTrack>().nextTarget - 1;
        // 시점 다시 맞추기
        Debug.Log("y : " + transform.rotation.y);
        transform.position = GameManager.Instance.target[target >= 0 ? target : 2].position;
        transform.rotation = GameManager.Instance.target[target >= 0 ? target : 2].rotation;
        moveSpeed = 1;
    }

    void GroundCheck() 
     {
        ray.direction = -transform.up;
        ray.origin = transform.position;
        if (Physics.CapsuleCast(ray.origin - transform.forward * (collider.height / 2 - collider.radius), ray.origin + transform.forward * 
             (collider.height / 2 - collider.radius), collider.radius, ray.direction,  out hitInfo, collider.radius + 1.1f, layerMask)) {
            Debug.Log("레이캐스트 히트");
            isGround = true;
        }
         else {
            Debug.Log("레이캐스트 실패");
            
            Debug.Log("distance : " + (collider.radius + 1.1f));
            isGround = false;
        }
     }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }

    /// <summary>
    /// 텍스트가 사용되는 부분들을 담당
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
    /// 바이크의 전체적인 이동을 관리하는 메소드
    /// 간단한 앞뒤, 양옆, 그리고 롤백을 담당한다.
    /// </summary>
    void Bicycle_Move() {
        if (isGround == false) {
            moveSpeed -= Time.fixedDeltaTime * (moveSpeed / 8);
        }

        if (gameObject.transform.position.y < -10)
            Rollback();
       else  if (270f > gameObject.transform.rotation.eulerAngles.z &&  gameObject.transform.rotation.eulerAngles.z > 90f&& isGround)
            Rollback();

        if (Input.GetKeyDown(KeyCode.R)) {
            Rollback();
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            //좌측 회전
            transform.Rotate(0, -1.5f, 0);

        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            //우측 회전
            transform.Rotate(0, 1.5f, 0);

        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            //가속도
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
            //이동속도가 1보다 크면 이동속도 비례해서 감속, 1보다 작으면 0으로 취급
            moveSpeed -= moveSpeed > 1 ? (moveSpeed / 100) : 0;
            time_right = 0;
            time_left = 0;
        }

        transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.DownArrow) && moveSpeed > -10) {
            // moveSpeed = moveSpeed > 0 ? moveSpeed -= (moveSpeed / 10) : moveSpeed > -10 ? -Time.deltaTime * 8 : moveSpeed = -10;

            //무브 스피드가 0보다 클땐 이동속도에 비례해서 감속하고, 0보다 작을경우 최대 -10까지만 이동
            if (moveSpeed > 1) {
                moveSpeed -= moveSpeed / 100;
            }
            else {
                moveSpeed -= Time.fixedDeltaTime * 8;
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
    /// <param name="direction"></param;
    private void Turn(ref float time, string direction) {
        //감속
        moveSpeed -= moveSpeed / 200 + time * moveSpeed / 200;

        Debug.Log(time * moveSpeed / 100 + "만큼 감속");
        float buho = direction == "right" ? 1 : -1;
        
        time += Time.fixedDeltaTime;
        Debug.Log(direction + "로 기울기 지속시간 : " + time);
        transform.Rotate(0, 0, buho * -time / 2 + buho * -0.1f);
         transform.Rotate(0, (time < 0.5f ? buho * time : buho * time / 2 + buho * 0.3f + 0.1f * buho), 0);
    }
}

