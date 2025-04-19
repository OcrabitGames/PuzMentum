using UnityEngine;
using UnityEngine.UI;

namespace Links
{
    public class SliderBehaviour : MonoBehaviour
    {
        private Slider _slider;
        public string valueName;
        private float _value;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _slider = GetComponent<Slider>();
        
            _value = PlayerPrefs.GetFloat(valueName, 1f);
            if (_slider)
            {
                _slider.value = _value;
            }
        }
    }
}
