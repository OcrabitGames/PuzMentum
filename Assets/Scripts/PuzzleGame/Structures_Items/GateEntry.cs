using System;
using UnityEngine;

public class GateEntry : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.NextLevel(other.gameObject);
        }
    }
}
