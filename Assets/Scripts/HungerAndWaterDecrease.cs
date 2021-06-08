using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerAndWaterDecrease : MonoBehaviour
{
    public int hungerDecreaseAmount;   //Süre başı ne kadar azalıcak.
    public int thirstDecreaseAmount;
    public int healthDecreaseAmount;

    private void Start()
    {
        StartCoroutine(HungerDecreaseLooper());
        StartCoroutine(ThirstDecreaseLooper());
        StartCoroutine(HealthDecreaseLooper());
    }
    private void Update()
    {
        int natural = (int)(GameObject.FindObjectOfType<DayNightCycle>().sun.intensity * 40);
        GameManager.playerData.temperature = natural + GameManager.playerData.extraTemparature; //Derece güneş parlaklığına ve kamp ateşinde olmasına göre hesaplanıt.
        GameManager.playerData.temperature = Mathf.Clamp(GameManager.playerData.temperature,2,35); //Derce en az 2 en fazla 35 olabilir.

        GameManager.playerData.playerHealth = Mathf.Clamp(GameManager.playerData.playerHealth,0,100);
    }
    private IEnumerator HungerDecreaseLooper()
    {
        yield return new WaitForSeconds(4f);
        if (GameManager.playerData.playerHunger > hungerDecreaseAmount) GameManager.playerData.playerHunger -= hungerDecreaseAmount; //Miktar eklenidğinde yüzü geçmemesi için bir koşul.
        else GameManager.playerData.playerHunger = 0;
        StartCoroutine(HungerDecreaseLooper());
    }
    private IEnumerator ThirstDecreaseLooper()
    {
        yield return new WaitForSeconds(4f);
        if (GameManager.playerData.playerThirst > thirstDecreaseAmount) GameManager.playerData.playerThirst -= thirstDecreaseAmount; //Miktar eklenidğinde yüzü geçmemesi için bir koşul.
        else GameManager.playerData.playerThirst = 0;
        StartCoroutine(ThirstDecreaseLooper());
    }
    private IEnumerator HealthDecreaseLooper()
    {
        yield return new WaitForSeconds(4f);
        if (GameManager.playerData.playerHunger <= 0) GameManager.playerData.playerHealth -= healthDecreaseAmount;
        if (GameManager.playerData.playerThirst <= 0) GameManager.playerData.playerHealth -= healthDecreaseAmount;
        if (GameManager.playerData.temperature <= 10) GameManager.playerData.playerHealth -= healthDecreaseAmount + 3; //3 tamamen keyfidir :>

        if (GameManager.playerData.playerHunger >= 75) GameManager.playerData.playerHealth += 1; //Susuzluk ve açlık bi seviyenin üstündeyse can artat.
        if (GameManager.playerData.playerThirst >= 75) GameManager.playerData.playerHealth += 1;

        StartCoroutine(HealthDecreaseLooper());
    }
}
