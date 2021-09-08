using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jumpSpeed;

    /// <summary>
    /// 세로 화면 전환
    /// </summary>
    [SerializeField]
    float speedH = 1.0f;

    /// <summary>
    /// 가로 화면 전환
    /// </summary>
    [SerializeField]
    float speedV = 1.0f;

    float yaw = 0.0f;
    private float pitch = 0.0f;

    private bool waterCheck;

    Vector3 moveDirection;
    CharacterController character;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Player_Move();
    }

    private void Player_Move() {
        if (waterCheck)
            WaterMove();
        else
            CommonMove();
        Mouse_Screen();
    }

    private void Mouse_Screen() {

        yaw += speedH * Input.GetAxis("Mouse X");

        // 플레이어의 x축이 35f를 넘어가면 더 이상 기울여지지 않음
        pitch -= Mathf.Abs(gameObject.transform.rotation.eulerAngles.x) <= 35
            || Mathf.Abs(gameObject.transform.rotation.eulerAngles.x) >= 325 ?
            speedV * Input.GetAxis("Mouse Y") : pitch / 20;

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    private void CommonMove() {

        if (gravity != 10)
            gravity = 10;
        if (character.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
            if (Input.GetKeyDown(KeyCode.Space)) {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        character.Move(moveDirection * Time.deltaTime);
    }

    private void WaterMove() { 
        transform.position += Vector3.down * gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R)) {

        }
        if (Input.GetKey(KeyCode.D)) {
            //좌측 회전
            transform.Rotate(0, -1.5f, 0);

        }
        else if (Input.GetKey(KeyCode.A)) {
            //우측 회전
            transform.Rotate(0, 1.5f, 0);

        }

        if (Input.GetKey(KeyCode.W)) {
            gravity = -0.1f;
            character.Move(Vector3.forward * moveSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.A)) {

            }
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("water")) {
            gravity = 1;
            waterCheck = true;
            Debug.Log("물속 확인");
        }
        else if(other.CompareTag("Ground")) {
            waterCheck = false;
            gravity = 10;
        }
        else {
            gravity = 10;
            waterCheck = false;
        }
    }
}
