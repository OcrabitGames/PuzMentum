using System;
using UnityEngine;

public class GateUnlocker : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (LevelManager.Instance.CheckConditions())
            {
                other.gameObject.GetComponent<KeyManager>().UnlockGate(gameObject);
            }
        }
    }
}
