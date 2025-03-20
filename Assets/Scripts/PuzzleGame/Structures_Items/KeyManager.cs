using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public List<KeyHandler> collectedKeys = new List<KeyHandler>();
    public float keyRadius = 1.5f;  // Radius of the key circle
    public float keyHeight = 2.0f;
    private GameObject gateToUnlock;
    
    public void AddKey(KeyHandler key)
    {
        if (!collectedKeys.Contains(key))
        {
            // Add Key To LevelManager
            LevelManager.Instance.CollectKey();
            
            collectedKeys.Add(key);
            RecalculateKeyPositions();
        }
    }
    
    private void RecalculateKeyPositions()
    {
        int keyCount = collectedKeys.Count;
        if (keyCount == 0) return;

        float angleStep = 360f / keyCount; // Evenly distribute keys in a circle
        for (int i = 0; i < keyCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 keyOffset = new Vector3(Mathf.Cos(angle) * keyRadius, keyHeight, Mathf.Sin(angle) * keyRadius);
            Vector3 targetPosition = transform.position + keyOffset; // Position relative to the player
            
            collectedKeys[i].SetTargetOffset(keyOffset); // Hand over the relative offset
        }
    }

    public void UnlockGate(GameObject gate)
    {
        gateToUnlock = gate;
        
        foreach (var key in collectedKeys)
        {
            key.StartUnlockingGate(gate.transform.position);
        }
    }
    
    public void RemoveKeyFromList(KeyHandler key)
    {
        // Remove key from the list
        collectedKeys.Remove(key);

        // If all keys reached the gate, unlock the gate
        if (collectedKeys.Count == 0)
        {
            gateToUnlock.SetActive(false);
        }
    }
}
