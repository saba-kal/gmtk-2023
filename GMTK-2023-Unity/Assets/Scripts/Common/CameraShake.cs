using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    [SerializeField] private float intensity = 2f;
    [SerializeField] private float duration = 0.5f;

    private CinemachineBasicMultiChannelPerlin cameraNoise;
    private float shakeTimer = 0f;

    private void Awake()
    {
        cameraNoise = GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Shake()
    {
        cameraNoise.m_AmplitudeGain = intensity;
        shakeTimer = duration;
        StartCoroutine(ReduceAmplitueOverTime());
    }

    private IEnumerator ReduceAmplitueOverTime()
    {
        while (shakeTimer > 0)
        {
            cameraNoise.m_AmplitudeGain = Mathf.Lerp(intensity, 0, (duration - shakeTimer) / duration);
            shakeTimer -= Time.deltaTime;
            yield return null;
        }
        cameraNoise.m_AmplitudeGain = 0;
    }
}
