using UnityEngine;

namespace PuzzleGame.Gimmicks
{
    public class MagneticMover : MonoBehaviour
    {
        public bool alwaysActive = false;
        
        private Transform _field;
        public float fieldCooldownTime = 3f;
        private float _fieldCooldownTime;
        private bool _fieldOnCooldown = false;
        
        private Renderer _fieldRenderer;
        [SerializeField] private Material activeMaterial;
        [SerializeField] private Material inactiveMaterial;
    
        private bool _isTriggered = false;
        private bool _isActivated = false;
        private bool _movingToEnd = true;
        private Transform _player;
    
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        public GameObject endTarget;
        private Vector3 _endPosition;
    
        public float speed = 3f;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _field = transform.Find("Field")?.gameObject.transform;
            if (_field) _fieldRenderer = _field.GetComponent<Renderer>();
            _fieldCooldownTime = fieldCooldownTime;
        
            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;
            _endPosition = transform.parent.InverseTransformPoint(endTarget.transform.position);

            // Set defualt state based on parameter
            if (!alwaysActive) {DeactivateField();} else {ActivateField();}
        }

        // Update is called once per frame
        void Update()
        {
            if (_isTriggered && _isActivated)
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
        
            Quaternion targetRotation = _movingToEnd ? endTarget.transform.rotation : _startRotation;
            float distanceToTarget = Vector3.Distance(transform.localPosition, target);
            float travelTime = distanceToTarget / speed; // Time required to reach the target
            float rotationSpeed = Quaternion.Angle(transform.rotation, targetRotation) / travelTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
            if (_player)
            {
                _player.position = _field.position + transform.up * 0.3f;
            }
        
            // Check if reached the destination
            if (Vector3.Distance(transform.localPosition, target) < 0.01f)
            {
                // Set final values
                transform.position = target;
                transform.rotation = targetRotation;
                
                _movingToEnd = !_movingToEnd;
                _isTriggered = false;
                
                if (_isTriggered) ActivateField();
            
                if (_player)
                {
                    _fieldOnCooldown = true;
                    _field.gameObject.SetActive(false);
                
                    _player.position = _field.position + transform.up * 0.3f;
                    _player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                    _player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    _player.SetParent(null);
                    _player = null;
                }
            }
        }

        public void TriggerMovement(Transform player)
        {
            _isTriggered = true;
            _player = player;
            _player.SetParent(transform);
        }

        public void ActivateField()
        {
            _isActivated = true; 
            _fieldRenderer.material = activeMaterial;

            if (!_movingToEnd)
            {
                QuarantineReturn();
            }
        }

        public void QuarantineReturn()
        {
            Debug.Log("Quarantine process activated: Ensuring platform returns safely.");
            
            // Cooldown Active Reset
            _field.gameObject.SetActive(true);
            _fieldOnCooldown = false;
            _fieldCooldownTime = fieldCooldownTime;
            
            // Trigger Movement
            _isTriggered = true;
        }

        public void DeactivateField()
        {
            if (!alwaysActive)
            {
                _isActivated = false;
                _fieldRenderer.material = inactiveMaterial;
            }
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

        public bool IsMoving()
        {
            return _isTriggered;
        }
    }
}
