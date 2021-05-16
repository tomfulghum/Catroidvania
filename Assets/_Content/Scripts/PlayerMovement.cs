using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask pushableMask;
    [SerializeField] LayerMask pushableCollisionMask;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] float tileSize = 0.5f;
    [SerializeField] float moveCooldownTime = 0.15f;

    float moveCooldownTimer = -1f;

    public delegate void PlayerMovedEvent(Vector2 newPosition);
    public static event PlayerMovedEvent OnPlayerMoved;

    void Start()
    {

    }

    void Update()
    {
        moveCooldownTimer -= Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (moveCooldownTimer > 0)
            return;

        var vector = context.ReadValue<Vector2>();
        if (vector == Vector2.zero)
            return;

        var direction = new Vector3();
        if (vector.x != 0)
            direction = new Vector3(Mathf.Sign(vector.x) * tileSize, 0, 0);
        else if (vector.y != 0)
            direction = new Vector3(0, Mathf.Sign(vector.y) * tileSize, 0);

        bool canMove = true;
        var pushableCollider = Physics2D.OverlapCircle(transform.position + direction, 0.1f, pushableMask);
        if (pushableCollider) {
            canMove = !Physics2D.OverlapCircle(transform.position + (direction * 2), 0.1f, pushableCollisionMask);
        }

        if (canMove && !Physics2D.OverlapCircle(transform.position + direction, 0.1f, collisionMask)) {
            var previousPosition = transform.position;
            transform.position += direction;
            OnPlayerMoved?.Invoke(transform.position);
            moveCooldownTimer = moveCooldownTime;
        }
    }
}
