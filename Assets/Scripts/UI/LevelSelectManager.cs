using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelSelectManager : MonoBehaviour
    {
        private LevelManager _levelManager;
        public GameObject[] levels;
        public Sprite starSprite;
        public TextMeshProUGUI starCountText;
    
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
            int numStars = 0;
            
            for (int i = 0; i < levels.Length; i++)
            {
                // Set Locked Status
                bool isLocked = i > highestLevel;
                levels[i].transform.Find("Lock").gameObject.SetActive(isLocked);
                if (!isLocked)
                {
                    // Set Best Time
                    var bestTime = PlayerPrefs.GetFloat($"BestTime_{i}", -1f);
                    if (bestTime < 0) break; // Break on never finished
                    
                    var timeObj = levels[i].transform.Find("Time");
                    if (timeObj)
                    {
                        var timeTMPro = timeObj.GetComponent<TextMeshProUGUI>();
                        timeTMPro.text = bestTime.ToString("00.00");
                        timeObj.gameObject.SetActive(true);
                    }
                    
                    
                    // Set Stars
                    Transform stars = levels[i].transform.Find("Stars");
                    //print($"Best Time: {bestTime}");
                    float[] thresholds = _levelManager.GetThresholdsForLevel(i);
                    
                        for (int j = 0; j < 3; j++)
                        {
                            //print($"Checking for threshold {j}: {thresholds[j]}");
                            if (bestTime <= thresholds[j])
                            {
                                stars.GetChild(j).GetComponent<Image>().sprite = starSprite;
                                numStars++;
                                //print($"Set Star for {levels[i].name} for star {j}");
                            }
                            else
                            {
                                break; // Skip on fail
                            }
                        }
                }
            }
            // Set Star Count
            starCountText.text = numStars.ToString();
        }

        public void GoToLevel(int levelIndex)
        {
            _levelManager.LoadLevel(levelIndex);
        }
    
    }
}
