using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroyer : MonoBehaviour
{
    public float time;
    private void Start() //Verilen zaman sonunda imha olur
    {
        Destroy(gameObject, time);
    }
}
