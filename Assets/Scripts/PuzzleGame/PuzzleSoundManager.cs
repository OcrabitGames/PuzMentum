using UnityEngine;

namespace PuzzleGame
{
    public class PuzzleSoundManager : MonoBehaviour
    {
        public AudioClip PickUpSound;
        public AudioClip WinSound;
    
        AudioSource audioSource;
        
    
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayKeyPickupSound()
        {
            audioSource.PlayOneShot(PickUpSound);
        }

        public void PlayWinSound()
        {
            audioSource.PlayOneShot(WinSound);
        }
    }
}
