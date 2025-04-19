using System.Collections;
using Audio;
using UnityEngine;

namespace Links
{
    public class SettingsLink : MonoBehaviour
    {
    
        private SoundManager _soundManager;
        private float _currentSfxVolume;
        private float _currentMusicVolume;
        private bool _invertedY;
        private Coroutine _saveRoutine;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _soundManager = SoundManager.Instance;
        }
    
        public void SetSfxVolume(float value)
        {
            _currentSfxVolume = value;
            print("Sfx Volume: " + value);
            ScheduleSave();
        }
    
        public void SetMusicVolume(float value)
        {
            _currentMusicVolume = value;
            print("Music Volume: " + value);
            ScheduleSave();
        }
    
        public void SetInvertedYAxis(bool inverted)
        {
            _invertedY = inverted;
            ScheduleSave();
        }
    
        private void ScheduleSave()
        {
            if (_saveRoutine != null)
                StopCoroutine(_saveRoutine);

            _saveRoutine = StartCoroutine(DelayedSave());
        }
    
        private IEnumerator DelayedSave()
        {
            yield return new WaitForSeconds(0.5f); // debounce delay

            PlayerPrefs.SetFloat("SfxVolume", _currentSfxVolume);
            PlayerPrefs.SetFloat("MusicVolume", _currentMusicVolume);
            PlayerPrefs.SetInt("InvertedYAxis", _invertedY ? 1 : 0);
            PlayerPrefs.Save();
            // Update Use Cases
            _soundManager.UpdateSoundSettings();

            _saveRoutine = null;
        }
    }
}
