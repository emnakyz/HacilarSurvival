using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHealth : MonoBehaviour
{
    public int increaseValue=10;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>() != null)
        {
            GameManager.playerData.playerHealth += increaseValue;
            Destroy(gameObject);
        }
    }
}
