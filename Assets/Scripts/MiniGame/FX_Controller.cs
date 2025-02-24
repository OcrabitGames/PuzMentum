using UnityEngine;

public class FX_Controller : MonoBehaviour
{
    public ParticleSystem FX_Collect;
    public ParticleSystem FX_Death;
    public ParticleSystem FX_Win;
    public ParticleSystem FX_Trail;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play_collect()
    {
        FX_Collect.Play();
    }
    
}
