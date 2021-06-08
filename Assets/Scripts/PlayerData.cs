using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int playerHealth { get; set; }  //Oyuncu canı
    public int playerHunger { get; set; }  //Oyuncu açlığı.
    public int playerThirst { get; set; }  //Oyuncu susuzluğu.
    public int temperature { get; set; }  //Derce
    public int extraTemparature { get; set; }  //Extra derece (kamp ateşi çevresindeyken artar)s
    
    public Tool currentUsedTool { get; set; }   //Mevcut kullanılan alet.

    public static float musicVolume { get; set; }  //Müzik sesi
    public static float sfxVolume { get; set; }  //Sfx sesi.
    public enum Tool
    {
        Axe,Pickaxe,Bow,None
    }
    private void Awake()  //Başta değerler bunlara eşitlenir.
    {
        extraTemparature = 0;
        playerHealth = 100;
        playerHunger = 100;
        playerThirst = 100;
        currentUsedTool = Tool.Axe;
    }
}
