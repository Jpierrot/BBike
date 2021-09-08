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
    /// ���� ȭ�� ��ȯ
    /// </summary>
    [SerializeField]
    float speedH = 1.0f;

    /// <summary>
    /// ���� ȭ�� ��ȯ
    /// </summary>
    [SerializeField]
    float speedV = 1.0f;

    float yaw = 0.0f;
    private float pitch = 0.0f;

    private bool waterCheck;

    Vector3 moveDirection;
    CharacterController character;


    // Start is called before the first frame update
    void Start() {
        
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        CommonMove();
        Mouse_Screen();
    }

    private void Mouse_Screen() {

        yaw += speedH * Input.GetAxis("Mouse X");

        // �÷��̾��� x���� 35f�� �Ѿ�� �� �̻� ��￩���� ����
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

}

