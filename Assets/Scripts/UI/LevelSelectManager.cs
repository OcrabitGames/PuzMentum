using UnityEngine;

namespace UI
{
    public class LevelSelectManager : MonoBehaviour
    {
        private LevelManager _levelManager;
        public GameObject[] levels;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _levelManager = LevelManager.Instance;
            UpdateLocks();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void UpdateLocks()
        {
            int highestLevel = _levelManager.GetHighestUnlockedLevel();
    
            for (int i = 0; i < levels.Length; i++)
            {
                bool isLocked = i > highestLevel;
                levels[i].transform.Find("Lock").gameObject.SetActive(isLocked);
            }
        }

        public void GoToLevel(int levelIndex)
        {
            _levelManager.LoadLevel(levelIndex);
        }
    
    }
}
