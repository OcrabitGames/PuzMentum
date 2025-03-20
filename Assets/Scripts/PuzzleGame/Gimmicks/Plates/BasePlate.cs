using UnityEngine;

namespace PuzzleGame.Gimmicks.Plates
{
    public abstract class BasePlate : MonoBehaviour
    {
        protected bool activated = false;
        private int lastTriggerFrame = 0;
        private int frameThreshold = 100;

        protected virtual void Update()
        {
            if (activated && Time.frameCount - lastTriggerFrame > frameThreshold)
            {
                activated = false;
                DeactivateTarget();
            }
        }

        protected virtual void OnTriggerStay(Collider collider)
        {
            if (!activated && collider.gameObject.CompareTag("AfterImage"))
            {
                activated = true;
                ActivateTarget();
            }
            lastTriggerFrame = Time.frameCount;
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
