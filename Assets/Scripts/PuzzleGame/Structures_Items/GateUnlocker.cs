using System;
using Audio;
using UnityEngine;

namespace PuzzleGame.Structures_Items
{
    public class GateUnlocker : MonoBehaviour
    {
        public bool type1Gate = true;
        private string _soundName;
        private void Start()
        {
            _soundName = $"GateUnlock#{(type1Gate ? 1 : 2)}";
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (LevelManager.Instance.CheckConditions())
                {
                    LevelManager.Instance.CheckSaveRoundTime();
                    LevelManager.Instance.CheckSaveNextLevel();
                    SoundManager.Instance.GenericPlaySound(_soundName, 0.3f);
                    other.gameObject.GetComponent<KeyManager>().UnlockGate(gameObject);
                }
            }
        }
    }
}
