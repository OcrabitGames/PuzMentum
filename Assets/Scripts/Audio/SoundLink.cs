using UnityEngine;

namespace Audio
{
    public class SoundLink : MonoBehaviour
    {
        private SoundManager _soundManager;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _soundManager = SoundManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void PlayClick()
        {
            _soundManager.PlayClick();
        }
    }
}
