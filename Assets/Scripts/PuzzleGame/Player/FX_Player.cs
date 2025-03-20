using UnityEngine;

public class FX_Player : MonoBehaviour
{
    public ParticleSystem FX_Collect;
    public ParticleSystem FX_Death;
    public ParticleSystem FX_Win;
    public GameObject parent_container;

    public void PlayFXCollect()
    {
        center_player();
        FX_Collect.Play();
    }
    
    public void PlayFXDeath()
    {
        center_player();
        FX_Death.Play();
    }
    
    public void PlayFXWin()
    {
        center_player();
        FX_Win.Play();
    }

    void center_player()
    {
        parent_container.transform.position = transform.position;
    }
}
