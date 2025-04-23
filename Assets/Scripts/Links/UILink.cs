using UnityEngine;

namespace Links
{
    public class UILink : MonoBehaviour
    {
        private LevelManager _levelManager;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _levelManager = LevelManager.Instance;
        }

        public void ReturnToMainMenu()
        {
            
            _levelManager.GoToMenu();
        }
    }
}
