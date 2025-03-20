using UnityEngine;

namespace PuzzleGame.Gimmicks.Plates
{
    public class RedPlate : BasePlate
    {
        public GameObject target;
        
        Vector3 _activatedPosition;
        Vector3 _originalPosition;

        private void Start()
        {
            // Set positions
            _originalPosition = target.transform.position;
            _activatedPosition = target.transform.position;
            _activatedPosition.y = -0.99f;
        }

        protected override void OnActivate()
        {
            target.transform.position = _activatedPosition;
        }

        protected override void OnDeactivate()
        {
            target.transform.position = _originalPosition;
        }
    }
}
