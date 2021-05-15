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
    [SerializeField] float tileSize = 0.5f;
    [SerializeField] float moveCooldownTime = 0.15f;

    public bool IsLeader { get; set; }

    public CongaCat follower;

    Vector3 targetPosition;
    Vector3 previousPosition;
    bool willBeLeader;

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
        if (willBeLeader) { 
            IsLeader = true;
            willBeLeader = false;
        }

        var speed = (tileSize / moveCooldownTime) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
    }

    void OnPlayerMoved(Vector2 newPosition)
    {
        if (IsLeader) {
            MoveCongaLine(new Vector3(newPosition.x, newPosition.y, -1));
        }
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
        if (!context.started)
            return;

        if (IsLeader) {
            Debug.Log("Fuck this");

            if (!follower)
                return;

            Debug.Log("Fuck that");

            var last = FindLast();
            MoveCongaLine(last.targetPosition);

            IsLeader = false;
            follower.willBeLeader = true;
            last.follower = this;
            follower = null;
        }
    }
}
