using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool isLockedForSpawner; //Spawner kırılmadan açılamasın mı ?
    [SerializeField] private MonsterSpawner spawner; //Bağlı spawner
    private bool inRange = false; //Mesafeye oyuncu girdimi

    public List<Item> loots = new List<Item>(); //İçindeki itemler
    private void Update()
    {
        if (inRange) //Mesafeyegirdiyse
        {
            if (!isLockedForSpawner) //Kitli değil ise
            {
                if (Input.GetKeyDown(KeyCode.E)) //E ye basar ise
                {
                    for (int i = 0; i < loots.Count; i++) //Tüm itemleri al.
                    {
                        GameManager.inventorySystem.AddItem(loots[i]);
                    }
                    Destroy(gameObject);
                }
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.E) && spawner == null)
                {
                    for (int i = 0; i < loots.Count; i++)
                    {
                        GameManager.inventorySystem.AddItem(loots[i]);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col) //İçerdeyken içerde dışarda iken dışarda bunu ölçmek için (mesafe)
    {
        if (col.gameObject.GetComponent<Player>() != null)
        {
            inRange = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>() != null)
        {
            inRange = false;
        }
    }
}
