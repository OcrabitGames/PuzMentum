using UnityEngine;

public class MagneticMover : MonoBehaviour
{
    private Transform _field;
    public float fieldCooldownTime = 3f;
    private float _fieldCooldownTime;
    private bool _fieldOnCooldown = false;
    
    private bool _isActivated = false;
    private bool _movingToEnd = true;
    private Transform _player;
    
    private Vector3 _startPosition;
    public GameObject endTarget;
    private Vector3 _endPosition;
    
    public float speed = 3f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _field = transform.Find("Field")?.gameObject.transform;
        _fieldCooldownTime = fieldCooldownTime;
        
        _startPosition = transform.position;
        _endPosition = endTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActivated)
        {
            MoveObject();
        }

        HandleFieldCooldown();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void MoveObject()
    {
        Vector3 target = _movingToEnd ? _endPosition : _startPosition;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (_player)
        {
            _player.position = _field.position + new Vector3(0, 0.3f, 0);
        }
        
        // Check if reached the destination
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            _movingToEnd = !_movingToEnd;
            
            _isActivated = false;
            if (_player)
            {
                _fieldOnCooldown = true;
                _field.gameObject.SetActive(false);
                _player.position = _field.position + new Vector3(0, 0.3f, 0);
                _player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                _player.SetParent(null);
                _player = null;
            }
        }
    }

    public void ActivateMovement(Transform player)
    {
        _isActivated = true;
        _player = player;
        _player.SetParent(transform);
    }

    public void ReleasePlayer()
    {
        if (_player)
        {
            _player.SetParent(null);
            _player = null;
        }
    }

    private void HandleFieldCooldown()
    {
        if (_fieldOnCooldown)
        {
            if (_fieldCooldownTime > 0)
            {
                _fieldCooldownTime -= Time.deltaTime;
            }
            else
            {
                _fieldOnCooldown = false;
                _fieldCooldownTime = fieldCooldownTime;
                _field.gameObject.SetActive(true);
            }
        }
    }
}
