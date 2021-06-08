using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneHandler : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel; //Paneller
    [SerializeField] private GameObject craftPanel;

    [SerializeField] private GameObject pausedPanel;
    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //I ya basıldığında envanter paneli açanır kapanır
        {
            if (!inventoryPanel.activeSelf)
            {
                GameManager.inventoryDisplayer.RefreshDisplay();
                inventoryPanel.SetActive(true);
                craftPanel.SetActive(false);
            }
            else
            {
                inventoryPanel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.C)) //Ayınısı üretim paneli içinde geçerli.
        {
            if (!craftPanel.activeSelf)
            {
                craftPanel.SetActive(true);
                GameManager.inventoryDisplayer.RefreshDisplay();
                inventoryPanel.SetActive(false);
            }
            else
            {
                craftPanel.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausedPanel.SetActive(true);
        }
    }
    public void Resume()  //Butonlar
    {
        Time.timeScale = 1;
        pausedPanel.SetActive(false);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void CloseCraftPanel()
    {
        craftPanel.SetActive(false);
    }
    public void CloseInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }
}
