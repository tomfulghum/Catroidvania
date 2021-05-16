using UnityEngine;
using UnityEngine.InputSystem;

public enum CatType
{
    Sowa,
    Chonk,
    Small,
    Push
}

public class CongaCat : MonoBehaviour
{
    [SerializeField] CatType type;
    [SerializeField] LayerMask catPickupMask;
    [SerializeField] float tileSize = 0.5f;
    [SerializeField] float moveCooldownTime = 0.15f;

    public CatType Type => type;
    public bool IsLeader { get; set; }
    public bool WillBeLeader { get; set; }

    public CongaCat follower;

    Vector3 targetPosition;
    Vector3 previousPosition;


    void OnEnable()
    {
        PlayerMovement.OnPlayerMoved += OnPlayerMoved;
    }

    void OnDisable()
    {
        PlayerMovement.OnPlayerMoved -= OnPlayerMoved;
    }

    void Start()
    {
        targetPosition = transform.position;
        if (type == CatType.Sowa)
            IsLeader = true;
    }

    void Update()
    {
        if (WillBeLeader) {
            IsLeader = true;
            WillBeLeader = false;
        }

        var speed = (tileSize / moveCooldownTime) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);

        if (IsLeader) {
            var pickup = Physics2D.OverlapCircle(targetPosition, 0.1f, catPickupMask);
            if (pickup) {
                var congaCat = pickup.GetComponent<CongaCat>();
                var last = FindLast();
                last.follower = congaCat;
                congaCat.targetPosition = last.previousPosition;
                congaCat.gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }
    }

    void OnPlayerMoved(Vector2 newPosition)
    {
        if (IsLeader)
            MoveCongaLine(new Vector3(newPosition.x, newPosition.y, -1));
    }

    void MoveCongaLine(Vector3 newPosition)
    {
        previousPosition = targetPosition;
        targetPosition = newPosition;

        if (follower)
            follower.MoveCongaLine(previousPosition);
    }

    CongaCat FindLast()
    {
        if (!follower)
            return this;

        return follower.FindLast();
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (!context.started || !IsLeader || !follower)
            return;

        var last = FindLast();
        MoveCongaLine(last.targetPosition);

        IsLeader = false;
        follower.WillBeLeader = true;
        last.follower = this;
        follower = null;
    }

    public void OnLevelFinished()
    {
        IsLeader = false;
    }
}
