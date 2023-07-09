using UnityEngine;


public class PlayAudioAtRandomIntervals : MonoBehaviour
{
    [SerializeField] private string soundEventName;

    public float minInterval = 5f; // Minimum time interval in seconds
    public float maxInterval = 10f; // Maximum time interval in seconds

    private float nextPlayTime; // Time when the next audio play should occur


    private void Start()
    {
        // Set the initial time for the first audio play
        nextPlayTime = Time.time + Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        // Check if it's time to play the audio
        if (Time.time >= nextPlayTime)
        {
            PlayAudio();

            // Calculate the next play time
            nextPlayTime = Time.time + Random.Range(minInterval, maxInterval);
        }
    }

    private void PlayAudio()
    {
        AkSoundEngine.PostEvent(soundEventName, gameObject);
    }
}