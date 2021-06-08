using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public Light2D sun;

    private float clock = 8;

    private bool cylcleTurn = true;
    private void Start()
    {
        StartCoroutine(ClockLoop());
    }
    private void Update()
    { 
        sun.intensity = clock / 24;
        sun.intensity = Mathf.Clamp(sun.intensity, 0.05f, 1.2f); //Saat sınırlandırılır.

        if (cylcleTurn) //Eğer gündüz oluyo ise bu değeri geçtimi artık gece olucak.
        {
            if (sun.intensity > 0.99f) cylcleTurn = false;
        }
        else  //Buda tam tersi.
        {
            if (sun.intensity < 0.07f) cylcleTurn = true;
        }
    }
    private IEnumerator ClockLoop() //Düzenli olarak güneş parlaklığı artar
    {
        yield return new WaitForSeconds(0.005f);
        if (cylcleTurn) clock += 0.005f;
        else clock -= 0.005f;
        StartCoroutine(ClockLoop());
    }
}
