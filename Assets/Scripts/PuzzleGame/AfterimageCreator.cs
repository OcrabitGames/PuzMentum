using UnityEngine;

public class AfterimageCreator : MonoBehaviour
{
    bool afterImageCreated = false;
    
    // Afterimage Prefab 
    public GameObject afterImagePrefab;
    GameObject afterImageContainer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool keyPress = Input.GetKeyDown(KeyCode.Space);
        if (keyPress)
        {
            SpawnAfterImage();
        }
    }

    void SpawnAfterImage()
    {
        if (!afterImageCreated)
        {
            afterImageContainer = Instantiate(afterImagePrefab, gameObject.transform.position, gameObject.transform.rotation);
            afterImageCreated = true;
        }
        else
        {
            Destroy(afterImageContainer);
            afterImageCreated = false;
            SpawnAfterImage();
        }
        
    }
}
