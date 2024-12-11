using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]
public class MovementController : MonoBehaviour
{
    [Header("InputManagerSO")]
    [SerializeField] private InputManagerSO _inputManagerSO;

    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _movementSpeed = 5.0f;

    private Collider2D _hit;
    private Vector2 _corner1;
    private Vector2 _corner2;

    private float _direction;

    private Rigidbody2D body2D;
    public Rigidbody2D Body2D {get{if(body2D == null) body2D = GetComponent<Rigidbody2D>(); return body2D;}}

    private BoxCollider2D boxCollider2D;
    public BoxCollider2D BoxCol2D {get{if(boxCollider2D == null) boxCollider2D = GetComponent<BoxCollider2D>(); return boxCollider2D;} }

    private Animator animator;
    public Animator Animator {get{if(animator == null) animator = GetComponentInChildren<Animator>(); return animator;}}

    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer {get{if(spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>(); return spriteRenderer;}}

    private CinemachineVirtualCamera cinemachineVitualCamera;
    public CinemachineVirtualCamera CinemachineVitualCamera {get{if(cinemachineVitualCamera == null)
                cinemachineVitualCamera = FindObjectOfType<CinemachineVirtualCamera>();return cinemachineVitualCamera;}}

    private void OnEnable()
    {
        CinemachineVitualCamera.Follow = gameObject.transform;

        _inputManagerSO.OnCharacterMove += OnHorizontalMovement;
        _inputManagerSO.OnCharacterJump += OnJump;
    }

    private void Start()
    {
        SpriteRenderer.flipY = false;
    }

    private void Update()
    {
        Body2D.velocity = new Vector2(_direction * _movementSpeed, Body2D.velocity.y);
    }

    public void OnHorizontalMovement(float horizontalInput)
    {
        ChengeAnimMoving(horizontalInput);

        _direction = horizontalInput;
    }

    public void OnJump()
    {
        CheckCollider2D();

        _hit = Physics2D.OverlapArea(_corner1, _corner2);

        if (_hit != null)
        {
            Animator.SetBool("isJump", true);
            Body2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        transform.parent = null;
    }

    private void CheckCollider2D()
    {
        Vector3 max = BoxCol2D.bounds.max;
        Vector3 min = BoxCol2D.bounds.min;
        _corner1 = new Vector2(max.x, min.y - .1f);
        _corner2 = new Vector2(min.x, min.y - .2f);
    }

    private void ChengeAnimMoving(float horizontalInput)
    {
        if (horizontalInput < 0 | horizontalInput > 0)
        {
            Animator.SetBool("isMoving", true);

            if (horizontalInput < 0)
            {
                SpriteRenderer.flipX = true;
            }
            if (horizontalInput > 0)
            {
                SpriteRenderer.flipX = false;
            }
        }
        if (horizontalInput == 0)
        {
            Animator.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Animator.SetBool("isJump", false);

        GameObject ground = collision.gameObject;

        if(ground.TryGetComponent<ObjectMovement>(out var movingPlatform))
        {
            transform.parent = movingPlatform.transform;
        }
        else
        {
            transform.parent = null;
        }
    }

    private void OnDisable()
    {
        _inputManagerSO.OnCharacterMove -= OnHorizontalMovement;
        _inputManagerSO.OnCharacterJump -= OnJump;
    }
}
