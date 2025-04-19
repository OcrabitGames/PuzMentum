using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [System.Serializable]
    public class SoundClipPair
    {
        public string name;
        public AudioClip clip;
    }

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
    
        private AudioSource _mainAudioSource;
        private AudioSource _sideAudioSource;
        public List<SoundClipPair> ClipList;
        private Dictionary<string, AudioClip> ClipDictionary;
        public bool onMenuScreen = true;
        private bool _cachedMenuBool = false;
        private float _sfxVolume;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            var audioSources = GetComponents<AudioSource>();
            _mainAudioSource = audioSources[0];
            _sideAudioSource = audioSources[1];
        
            // Set Initial Volumes
            _sideAudioSource.volume = PlayerPrefs.GetFloat("SfxVolume", 1f);
            _mainAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        
            // Set Clips
            ClipDictionary = new Dictionary<string, AudioClip>();
            foreach (var pair in ClipList)
            {
                if (!ClipDictionary.ContainsKey(pair.name))
                {
                    ClipDictionary[pair.name] = pair.clip;
                }
            }
            SetMainAudio();
        }
    
        public void SetMainAudio()
        {
            if (_cachedMenuBool != onMenuScreen)
            {
                // Set Cached Value
                _cachedMenuBool = onMenuScreen;
            
                print("Inside Cached Check");
                _mainAudioSource.Stop();
                _mainAudioSource.loop = true;
                _mainAudioSource.clip = onMenuScreen ? GenericGetSound("MenuMusic") : GenericGetSound("GameMusic"); 
                _mainAudioSource.Play();
            }
        }

        public void PlayClick()
        {
            var value = Random.Range(1, 4);
            GenericPlaySound($"Click{value}");
        }
        
        public void GenericPlaySound(string soundName)
        {
            if (ClipDictionary.TryGetValue(soundName, out AudioClip clip))
            {
                _sideAudioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning($"Clip '{soundName}' not found in ClipDictionary.");
            }
        }


        public void GenericPlaySound(string soundName, float volume)
        {
            if (ClipDictionary.TryGetValue(soundName, out AudioClip clip))
            {
                _sideAudioSource.PlayOneShot(clip, volume);
            }
            else
            {
                Debug.LogWarning($"Clip '{soundName}' not found in ClipDictionary.");
            }
        }
    
        private AudioClip GenericGetSound(string soundName)
        {
            if (ClipDictionary.TryGetValue(soundName, out AudioClip clip))
            {
                return clip;
            }
            else
            {
                Debug.LogWarning($"Clip '{soundName}' not found in ClipDictionary.");
                return null;
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void UpdateSoundSettings()
        {
            _sideAudioSource.volume = PlayerPrefs.GetFloat("SfxVolume", 1f);
            _mainAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        }
    }
}