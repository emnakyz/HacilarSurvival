using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    float shakeTimer;
    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCam(float intensity, float time) //�iddet ve zaman g�re cinamachine ile sars�lam verilir.
    {
        CinemachineBasicMultiChannelPerlin perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                perlin.m_AmplitudeGain = 0;
            }
        }
    }
}
