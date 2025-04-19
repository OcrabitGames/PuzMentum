using UnityEngine;

namespace PuzzleGame.Structures_Items
{
    public class KeyRotator : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3 (45, 45, 45) * Time.deltaTime);
        }
    }
}
