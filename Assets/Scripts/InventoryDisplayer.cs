using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject scrolContent;
    [SerializeField] private GameObject itemDisplayCellPrefab;

    public List<GameObject> itemCellsPrefabs = new List<GameObject>();


    public void RefreshDisplay()
    {
        for (int i = 0; i < itemCellsPrefabs.Count; i++) //İlk tüm listedeki objeleri siler obje olarak !
        {
            Destroy(itemCellsPrefabs[i]);
        }
        for (int i = 0; i < GameManager.inventorySystem.items.Count; i++) //Sonra tüm itemlar kadar eşya çerçevesi spawnlar ev üstündeki değerleri iteme göre düzenler.
        {
            GameObject itemCell = Instantiate(itemDisplayCellPrefab, scrolContent.transform.position, Quaternion.identity);
            itemCell.transform.parent = scrolContent.transform;
            itemCell.transform.Find("ItemName").gameObject.GetComponent<Text>().text = GameManager.inventorySystem.items[i].itemDisplayName;
            itemCell.transform.Find("ItemCount").gameObject.GetComponent<Text>().text = GameManager.inventorySystem.items[i].count.ToString();
            itemCell.transform.Find("ItemImg").gameObject.GetComponent<Image>().sprite = GameManager.inventorySystem.items[i].itemImg;
            var i2 = i;
            if (GameManager.inventorySystem.items[i2].canUsable)
                itemCell.transform.Find("UseBtn").gameObject.GetComponent<Button>().onClick.AddListener(delegate () { GameManager.inventorySystem.UseItem(GameManager.inventorySystem.items[i2]); });
            else itemCell.transform.Find("UseBtn").gameObject.GetComponent<Button>().interactable = false;
            itemCellsPrefabs.Add(itemCell);
        }
    }
}
