using UnityEngine;

namespace PuzzleGame.Gimmicks.Plates
{
    public abstract class BasePlate : MonoBehaviour
    {
        protected bool Activated = false;
        private int _lastTriggerFrame = 0;
        private int _frameThreshold = 100;

        protected virtual void Update()
        {
            if (Activated && Time.frameCount - _lastTriggerFrame > _frameThreshold)
            {
                Activated = false;
                DeactivateTarget();
            }
        }

        protected virtual void OnTriggerStay(Collider collider)
        {
            if (!Activated && collider.gameObject.CompareTag("AfterImage"))
            {
                Activated = true;
                ActivateTarget();
            }
            _lastTriggerFrame = Time.frameCount;
        }

        protected void ActivateTarget()
        {
            OnActivate();
        }

        protected void DeactivateTarget()
        {
            OnDeactivate();
        }

        protected abstract void OnActivate();
        protected abstract void OnDeactivate();
    }
}
