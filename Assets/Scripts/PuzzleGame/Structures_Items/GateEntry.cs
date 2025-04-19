using UnityEngine;

namespace PuzzleGame.Structures_Items
{
    public class GateEntry : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                LevelManager.Instance.NextLevel();
            }
        }
    }
}
