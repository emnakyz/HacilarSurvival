using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] //Sayesinde class inspectorda görünebilir
public class Item
{
    public string itemDisplayName; //Envanterde gözükecek isim.
    public string itemNormalName;  //Kod ismi
    public int count;  //Miktar
    public Sprite itemImg;  //Eşya görseli
    public bool canUsable;  //Kullanılabilir mi ?
}
