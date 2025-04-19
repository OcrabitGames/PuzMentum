using UnityEngine;
using UnityEngine.UI;

namespace Links
{
    public class ToggleBehaviour : MonoBehaviour
    {
        private Toggle _toggle;
        public string valueName;
        private float _value;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _toggle = GetComponent<Toggle>();
        
            if (_toggle)
            {
                _toggle.enabled = PlayerPrefs.GetInt(valueName, 1) == 1;
            }
        }
    }
}
