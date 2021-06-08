using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuging : MonoBehaviour
{     
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Item wood = new Item()
            {
                itemDisplayName = "Tahta",
                itemNormalName = "Wood",
                itemImg = GameManager.inventorySystem.woolSprite,
                count = 200,
                canUsable = false
            };
            Item stone = new Item()
            {
                itemDisplayName = "Taş",
                itemNormalName = "Stone",
                itemImg = GameManager.inventorySystem.stoneSprite,
                count = 200,
                canUsable = false
            };
            Item rope = new Item()
            {
                itemDisplayName = "İp",
                itemNormalName = "Rope",
                itemImg = GameManager.inventorySystem.ropeSprite,
                count = 200,
                canUsable = false
            };
            Item wool = new Item()
            {
                itemDisplayName = "Yün",
                itemNormalName = "Wool",
                itemImg = GameManager.inventorySystem.woolSprite,
                count = 200,
                canUsable = false
            };

            GameManager.inventorySystem.AddItem(wood);
            GameManager.inventorySystem.AddItem(stone);
            GameManager.inventorySystem.AddItem(rope);
            GameManager.inventorySystem.AddItem(wool);
            GameManager.inventoryDisplayer.RefreshDisplay();
        }    
    }
}
