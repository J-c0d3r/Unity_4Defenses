using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake instance { get; private set; }

    private float shakeTimer;
    private float shakeTimerTotal;
    private float startIntensity;
    private bool toLockShakeCam;

    private CinemachineBasicMultiChannelPerlin camShake;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        camShake = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            camShake.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));

            if (shakeTimer <= 0)
                toLockShakeCam = false;
        }
    }

    public void ShakeCamera(float intensity, float timeAmount)
    {
        if (!toLockShakeCam)
        {
            //toLockShakeCam = true;
            camShake.m_AmplitudeGain = intensity;
            startIntensity = intensity;
            shakeTimer = timeAmount;
            shakeTimerTotal = timeAmount;
        }

    }

    public void BossDieShakeCamera(float intensity, float timeAmount, bool toLock)
    {
        toLockShakeCam = toLock;
        camShake.m_AmplitudeGain = intensity;
        startIntensity = intensity;
        shakeTimer = timeAmount;
        shakeTimerTotal = timeAmount;
    }

}
