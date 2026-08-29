using UnityEngine;

public class GroundController : MonoBehaviour
{
    [Header("地面の設定")]
    [InspectorName("移動速度")]
    [SerializeField] private float _groundSpeed = 10f;
    [InspectorName("移動方向")]
    [SerializeField] private Vector3 _groundMoveDirection;

    private float _groundRadius;

    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _groundRadius = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveGround();
    }

    private void MoveGround()
    {
        _rigidbody.linearVelocity = _groundMoveDirection * _groundSpeed;
    }
}
