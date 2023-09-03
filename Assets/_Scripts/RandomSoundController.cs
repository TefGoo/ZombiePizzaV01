using UnityEngine;

public class RandomSoundController : MonoBehaviour
{
    public AudioClip[] enemySounds; // Array to hold your three audio clips
    public float minPitch = 0.8f;  // Minimum pitch for audio playback
    public float maxPitch = 1.2f;  // Maximum pitch for audio playback
    public float minVolume = 0.5f; // Minimum volume for audio playback
    public float maxVolume = 1.0f; // Maximum volume for audio playback
    public float minTimeBetweenSounds = 2.0f; // Minimum time between sounds
    public float maxTimeBetweenSounds = 5.0f; // Maximum time between sounds

    private AudioSource audioSource;
    private float timeSinceLastSound;
    private float timeToNextSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        RandomizeSound();
    }

    private void Update()
    {
        // Check if it's time to play the next sound
        timeSinceLastSound += Time.deltaTime;
        if (timeSinceLastSound >= timeToNextSound)
        {
            // Play a random sound with random pitch and volume
            RandomizeSound();
            audioSource.Play();
        }
    }

    private void RandomizeSound()
    {
        // Randomly select one of the audio clips
        int randomIndex = Random.Range(0, enemySounds.Length);
        audioSource.clip = enemySounds[randomIndex];

        // Randomly set pitch and volume within specified ranges
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.volume = Random.Range(minVolume, maxVolume);

        // Randomly set time between sounds within specified ranges
        timeToNextSound = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
        timeSinceLastSound = 0f;
    }
}
