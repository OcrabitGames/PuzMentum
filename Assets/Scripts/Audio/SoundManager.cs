using System.Collections;
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
    
        // Main Music
        public AudioClip[] mainClips;
        [SerializeField] private int currentMainIndex = 0;
        private int? _pendingLevelIndex = null;
        
        // Clips
        private AudioSource _mainAudioSource;
        private AudioSource _sideAudioSource;
        private AudioSource _levelAudioSource;
        public List<SoundClipPair> ClipList;
        private Dictionary<string, AudioClip> ClipDictionary;
        public bool onMenuScreen = true;
        private float _sfxVolume;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            var audioSources = GetComponents<AudioSource>();
            _mainAudioSource = audioSources[0];
            _sideAudioSource = audioSources[1];
            _levelAudioSource = audioSources[2];
        
            // Set Initial Volumes
            UpdateSoundSettings();
        
            // Set Clips
            ClipDictionary = new Dictionary<string, AudioClip>();
            foreach (var pair in ClipList)
            {
                if (!ClipDictionary.ContainsKey(pair.name))
                {
                    ClipDictionary[pair.name] = pair.clip;
                }
            }
            if (onMenuScreen)
            {
                PlayNextMainClip();
            }
        }

        private void PlayNextMainClip()
        {
            if (!CoreCanvasController.Instance.isLevel || PlayerPrefs.GetInt("ProgressiveSoundtrack", 0) == 0)
            {
                _mainAudioSource.clip = mainClips[currentMainIndex];
                _mainAudioSource.Play();
                StartCoroutine(ScheduleNextMainClip());
            }
        }

        private IEnumerator ScheduleNextMainClip()
        {
            double startTime = AudioSettings.dspTime;
            double endTime = startTime + _mainAudioSource.clip.length;

            yield return new WaitUntil(() => AudioSettings.dspTime >= endTime);

            if (_pendingLevelIndex.HasValue)
            {
                SetLevelMusic(_pendingLevelIndex.Value);
                _pendingLevelIndex = null;
            }
            else if (!CoreCanvasController.Instance.isLevel || PlayerPrefs.GetInt("ProgressiveSoundtrack", 0) == 0)
            {
                currentMainIndex = (currentMainIndex + 1) % mainClips.Length;
                PlayNextMainClip();
            }
        }

        public void HandleEnterLevel(int levelIndex)
        {
            if (_levelAudioSource.isPlaying)
            {
                StartCoroutine(ScheduleLevelMusic(levelIndex));
            }
            else
            {
                _pendingLevelIndex = levelIndex;
            }
        }

        private void SetLevelMusic(int index)
        {
            var clip = LevelManager.Instance.levels[index].levelAudio;
            _mainAudioSource.Pause();
            if (clip)
            {
                _levelAudioSource.clip = clip;
                _levelAudioSource.loop = true;
                _levelAudioSource.Play();
            }
        }

        private IEnumerator ScheduleLevelMusic(int index)
        {
            double curTime = AudioSettings.dspTime;
            double endTime = curTime + (_levelAudioSource.clip.length - _levelAudioSource.time);

            yield return new WaitUntil(() => AudioSettings.dspTime >= endTime);

            SetLevelMusic(index);
        }
        
        public void ReturnToMain()
        {
            _levelAudioSource.Stop();
            PlayNextMainClip();
        }
    
        // public void SetMainAudio()
        // {
        //     if (_cachedMenuBool != onMenuScreen)
        //     {
        //         // Set Cached Value
        //         _cachedMenuBool = onMenuScreen;
        //     
        //         print("Inside Cached Check");
        //         _mainAudioSource.Stop();
        //         _mainAudioSource.loop = true;
        //         _mainAudioSource.clip = onMenuScreen ? GenericGetSound("MenuMusic") : GenericGetSound("GameMusic"); 
        //         _mainAudioSource.Play();
        //     }
        // }

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
            var musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
            _mainAudioSource.volume = musicVol;
            _levelAudioSource.volume = musicVol;
        }
    }
}