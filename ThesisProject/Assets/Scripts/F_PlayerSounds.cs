using UnityEngine;
using UnityEngine.InputSystem;

public class F_PlayerSounds : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnMovement(InputValue input)
    {
        // Playing step sound when movement input is registered
        if (audioSource.clip != F_SoundManager.instance.stepAudio)
        {
            audioSource.clip = F_SoundManager.instance.stepAudio;
            audioSource.loop = true;
        }
        //preventing sound from starting over every time we press multiple buttons
        if (!audioSource.isPlaying) audioSource.Play();
    }
    private void OnMovementStop(InputValue input)
    {
        audioSource.Stop();
    }
}
