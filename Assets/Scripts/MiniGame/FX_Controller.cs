using UnityEngine;

public class FX_Controller : MonoBehaviour
{
    public ParticleSystem FX_Collect;
    public ParticleSystem FX_Death;
    public ParticleSystem FX_Win;
    public GameObject parent_container;

    public void play_collect()
    {
        FX_Collect.Play();
    }
    
    public void play_death()
    {
        FX_Death.Play();
    }
    
    public void play_win()
    {
        FX_Win.Play();
    }

    public void center_player(Transform player)
    {
        parent_container.transform.position = player.position;
    }
}
