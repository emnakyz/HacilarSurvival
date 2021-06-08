using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static PlayerData playerData { get; private set; }
    public static InventorySystem inventorySystem { get; private set; }
    public static InventoryDisplayer inventoryDisplayer { get; private set; }
    public static CamShake camShake { get; private set; }
    public static CraftingSystem craftingSystem { get; private set; }
    public static SoundSystem soundSytem { get; private set; }

    [SerializeField] private GameObject diedPanel;
    [SerializeField] private GameObject escapedPanel;
    public GameObject collectableHealthPref;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")  //Sahne oyun sahnesi ise bul
        {
            playerData = GameObject.FindObjectOfType<PlayerData>();
            inventorySystem = GameObject.FindObjectOfType<InventorySystem>();
            inventoryDisplayer = GameObject.FindObjectOfType<InventoryDisplayer>();
            camShake = GameObject.FindObjectOfType<CamShake>();
            craftingSystem = GameObject.FindObjectOfType<CraftingSystem>();
            soundSytem = GameObject.FindObjectOfType<SoundSystem>();
        }
    }
    public void GameOverWithLose() //Kaybedince
    {
        Time.timeScale = 0;
        diedPanel.SetActive(true);
    }
    public void GameOverWithWin()  //Kazanınca
    {
        Time.timeScale = 0;
        escapedPanel.SetActive(true);
    }
}
