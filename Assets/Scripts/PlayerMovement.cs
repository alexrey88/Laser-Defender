using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    [Header("Moving Region")]
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Vector2 rawMoveInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    void OnMove(InputValue value)
    {
        rawMoveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        shooter.isFiring = value.isPressed;
    }

    void Awake()
    {
        shooter = GetComponent<Shooter>();

        if (shooter == null)
        {
            Debug.LogError("Shooter component on Player is null.");
        }
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        minBounds = new Vector2(minBounds.x + paddingLeft, minBounds.y + paddingBottom);
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        maxBounds = new Vector2(maxBounds.x - paddingRight, maxBounds.y - paddingTop);
    }

    void Move()
    {
        Vector2 deltaPosition = rawMoveInput * moveSpeed * Time.deltaTime;
        transform.position = GetNewPositionInsideBounds(deltaPosition);
    }

    Vector2 GetNewPositionInsideBounds(Vector2 deltaPosition)
    {
        Vector2 newPosition = new Vector2();
        newPosition.x = Mathf.Clamp(transform.position.x + deltaPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(transform.position.y + deltaPosition.y, minBounds.y, maxBounds.y);
        return newPosition;
    }

    public Vector2 GetPlayerMinBounds()
    {
        return minBounds;
    }

    public Vector2 GetPlayerMaxBounds()
    {
        return maxBounds;
    }
}
