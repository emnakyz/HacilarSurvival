using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiDisplayer : MonoBehaviour
{
    [Header("Can")]   //Can yiyecek barları vs.
    [SerializeField] private Text healthTxt;
    [SerializeField] private Slider healthSlider;
    [Header("Açlık")]
    [SerializeField] private Text hungerTxt;
    [SerializeField] private Slider hungerSlider;
    [Header("Susuzluk")]
    [SerializeField] private Text thirstTxt;
    [SerializeField] private Slider thirstSlider;
    [Header("Derece")]
    [SerializeField] private Text temparatureTxt;
    [SerializeField] private Color coldColor;
    [SerializeField] private Color warmColor;
    [SerializeField] private Color hotColor;

    private void Update()
    {
        RefreshStats();
    }
    private void RefreshStats()  //Bu barlardaki veriler vs. değerler ile eşleşir.
    {
        healthTxt.text = GameManager.playerData.playerHealth.ToString();
        healthSlider.value = GameManager.playerData.playerHealth;

        hungerTxt.text = GameManager.playerData.playerHunger.ToString();
        hungerSlider.value = GameManager.playerData.playerHunger;

        thirstTxt.text = GameManager.playerData.playerThirst.ToString();
        thirstSlider.value = GameManager.playerData.playerThirst;

        if (GameManager.playerData.temperature <= 10)   //Burda ısının derecesine göre soğuk mu sıcak mı o gösterilir.
        {
            temparatureTxt.text = GameManager.playerData.temperature + "° " + " Soğuk";
            temparatureTxt.color = coldColor;
        }
        else if (GameManager.playerData.temperature > 10 && GameManager.playerData.temperature <= 22)
        {
            temparatureTxt.text = GameManager.playerData.temperature + "° " + " Ilık";
            temparatureTxt.color = warmColor;
        }
        else
        {
            temparatureTxt.text = GameManager.playerData.temperature + "° " + " Sıcak";
            temparatureTxt.color = hotColor;
        }
    }
}
