using Audio;
using UnityEngine;

namespace PuzzleGame
{
    public class AfterimageCreator : MonoBehaviour
    {
        private bool _afterImageCreated = false;
    
        // Afterimage Prefab 
        public GameObject afterImagePrefab;
        private GameObject _afterImageContainer;
        private SoundManager _soundManager;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _soundManager = SoundManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            bool keyPress = Input.GetKeyDown(KeyCode.Space);
            if (keyPress)
            {
                SpawnAfterImage();
                _soundManager.GenericPlaySound("AfterImage", 0.55f);
            }
        }

        void SpawnAfterImage()
        {
            if (_afterImageCreated)
            {
                Destroy(_afterImageContainer);
            }

            _afterImageContainer = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            _afterImageCreated = true;
        }
    }
}
