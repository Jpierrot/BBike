using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    /// <summary>
    /// �÷��̾�
    /// </summary>
    [SerializeField]
    GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = new Quaternion(transform.localRotation.x, 0, player.gameObject.transform.localRotation.y , player.gameObject.transform.localRotation.w);
        Debug.Log("�̴ϸ� " + player.gameObject.transform.eulerAngles.y + 180f);
        gameObject.transform.position = (new Vector3(player.transform.position.x , gameObject.transform.position.y , player.transform.position.z));
    }
}
