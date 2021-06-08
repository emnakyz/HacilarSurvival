using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private Button campFireBtn;
    [SerializeField] private Button arrowBtn;
    private void Update()
    {
        if (GameManager.inventorySystem.GetItemByName("Wood") != null && GameManager.inventorySystem.GetItemByName("Stone") != null) //Craftlanabilir eşyaların craft butonları aktif mi pasif mi olcauk.
        {
            if (GameManager.inventorySystem.GetItemByName("Wood").count >= 24 && GameManager.inventorySystem.GetItemByName("Stone").count >= 16)
                campFireBtn.interactable = true;
            else campFireBtn.interactable = false;
        }
        if (GameManager.inventorySystem.GetItemByName("Wood") != null && GameManager.inventorySystem.GetItemByName("Stone") != null)
        {
            if (GameManager.inventorySystem.GetItemByName("Wood").count >= 9 && GameManager.inventorySystem.GetItemByName("Stone").count >= 3)
                arrowBtn.interactable = true;
            else arrowBtn.interactable = false;
        }
    }
    //Craftlanan ürünler.
    public void CraftCampFire()
    {
        if (GameManager.inventorySystem.GetItemByName("Wood").count >= 24 && GameManager.inventorySystem.GetItemByName("Stone").count >= 16) //Yeterli sayıda varsa
        {
            Item item = new Item()
            {
                itemDisplayName = "Kamp Ateşi",
                itemNormalName = "CampFire",
                count = 1,
                itemImg = GameManager.inventorySystem.campFire,
                canUsable = true
            };
            GameManager.inventorySystem.AddItem(item);
            GameManager.inventorySystem.GetItemByName("Wood").count -= 24;
            GameManager.inventorySystem.GetItemByName("Stone").count -= 16;
        }
    }
    public void CraftArrow()
    {
        if (GameManager.inventorySystem.GetItemByName("Wood").count >= 9 && GameManager.inventorySystem.GetItemByName("Stone").count >= 3)
        {
            Item item = new Item()
            {
                itemDisplayName = "Ok",
                itemNormalName = "Arrow",
                count = 6,
                itemImg = GameManager.inventorySystem.arrowSprite,
                canUsable = false
            };
            GameManager.inventorySystem.AddItem(item);
            GameManager.inventorySystem.GetItemByName("Wood").count -= 9;
            GameManager.inventorySystem.GetItemByName("Stone").count -= 3;
        }
    }
    public void CraftBoat()
    {
        if (GameManager.inventorySystem.GetItemByName("Wood").count >= 900 && GameManager.inventorySystem.GetItemByName("Stone").count >= 500 &&
            GameManager.inventorySystem.GetItemByName("Rope").count >= 10 && GameManager.inventorySystem.GetItemByName("Wool").count >= 50)
        {
            GameObject.FindObjectOfType<GameManager>().GameOverWithWin();
        }
    }
}
