using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col) //Oyuncu içindeyse ekstra deecrece artar.
    {
        if (col.gameObject.GetComponent<Player>() != null)
        {
            GameManager.playerData.extraTemparature = 7;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>() != null)
        {
            GameManager.playerData.extraTemparature = 0;
        }
    }
}
