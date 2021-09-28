using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public GameObject Player;
    public float speed;

    // Start is called before the first frame update
    private void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate() {
        follow();
    }

    private void follow() {
        gameObject.transform.position = 
            Vector3.Lerp(transform.position, Player.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(Player.gameObject.transform, transform.position);
    }

}
