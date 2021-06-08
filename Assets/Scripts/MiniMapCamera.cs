using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    private Transform player;
    public float offset = 0;
    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>().gameObject.transform;
    }

    private void LateUpdate() //Oyunucuyu takip etmesini sağlar
    {
        Vector3 temp = transform.position;
        temp.x = player.position.x;
        temp.x += offset;
        temp.y = player.position.y;
        temp.y += offset;
        transform.position = temp;
    }
}
