using UnityEngine;

namespace PuzzleGame.Gimmicks.Plates
{
    // Note should only be used for MagneticMovers
    public class OrangePlate : BasePlate
    {
        public GameObject target;
        private MagneticMover _magneticMover;

        void Start()
        {
            _magneticMover = target.GetComponent<MagneticMover>();
        }

        protected override void OnActivate()
        {
            if (_magneticMover) _magneticMover.ActivateField();
        }

        protected override void OnDeactivate()
        {
            if (_magneticMover) _magneticMover.DeactivateField();
        }
    }
}
