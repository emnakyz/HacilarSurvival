using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<Item> items = new List<Item>();  //Envanter (eşyalar)

    [Header("Eşya Görselleri")] //Eşya görselleri
    [SerializeField] public Sprite woodSprite;
    [SerializeField] public Sprite stoneSprite;
    public Sprite campFire;
    public Sprite arrowSprite;
    public Sprite fruitSprite;

    [Header("Yerleştirilebilir")]  //Yerleştirilebilir eşyaların prefabları.
    [SerializeField] private GameObject campFirePref;

    [Header("Sesler")]  //Sesler
    [SerializeField] private AudioClip placedSound;
    [SerializeField] private AudioClip eatSound;
    internal Sprite woolSprite;
    internal Sprite ropeSprite;

    public void AddItem (Item item) //Verilen item eklenir.
    {
        for (int i = 0;i < items.Count; i++) 
        {
            if (items[i].itemNormalName == item.itemNormalName) //Eğer envanterde aynı item varsa gelen eşya kadar o eşyanın miktarı artar.
            {
                items[i].count += item.count;
                return;
            }
        }
        items.Add(item);  //Yok ise doğrudan eklenir.
    }
    public Item GetItemByName(string itemName)  //İsme göre geri item döndürür (varsa)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemNormalName == itemName)
            {
                return items[i];
            }
        }
        return null;
    }
    public void DeleteItem(string itemName,int count) //İsme göre girilen miktarda item siler.
    {
        for (int i = 0;i < items.Count; i++)
        {
            if (items[i].itemNormalName == itemName)
            {
                items[i].count -= count;
                if (items[i].count <= 0) items.RemoveAt(i);  //Eğer 0 yada daha az kaldıysa item sil.
            }
        }
    }
    public void UseItem(Item item) //Verilen eşyaya göre kullanılır.
    {
        if (item.itemNormalName == "CampFire")
        {
            GameManager.soundSytem.PlaySfx(placedSound);
            Instantiate(campFirePref,GameObject.FindObjectOfType<Player>().gameObject.transform.position,Quaternion.identity);
            DeleteItem("CampFire",1);
            GameManager.inventoryDisplayer.RefreshDisplay();
        }
        else if (item.itemNormalName == "Fruit")
        {
            GameManager.soundSytem.PlaySfx(eatSound);
            if (GameManager.playerData.playerHunger <= 95) GameManager.playerData.playerHunger += 5;
            else GameManager.playerData.playerHunger = 100;
            DeleteItem("Fruit", 1);
            GameManager.inventoryDisplayer.RefreshDisplay();
        }
    }
    public Sprite GetItemSprite(string itemName) //Eşya görsellerini döndürür isme göre
    {
        if (itemName == "Wood") return woodSprite;
        else if (itemName == "Stone") return stoneSprite;
        else return null;
    }
}
