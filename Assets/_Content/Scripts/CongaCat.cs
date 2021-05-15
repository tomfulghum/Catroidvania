using UnityEngine;

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
    [SerializeField] bool isLeader;

    public bool IsLeader { get; set; }
    public bool IsLast => follower == null;

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
    }

    void Update()
    {
        var speed = (tileSize / moveCooldownTime) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
    }

    void OnPlayerMoved(Vector2 newPosition)
    {
        if (IsLeader || isLeader) {
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
}
