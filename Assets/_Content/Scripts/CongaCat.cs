using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CatType
{
    Sowa,
    Chonk,
    Small,
    Push,
    Useless
}

public class CongaCat : MonoBehaviour
{
    [SerializeField] CatType type;
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask catPickupMask;
    [SerializeField] float tileSize = 0.5f;
    [SerializeField] float moveCooldownTime = 0.15f;

    CongaCat follower;
    Vector3 spawnPosition;
    Vector3 targetPosition;
    Vector3 previousPosition;

    bool isMoving;

    public CatType Type => type;
    public bool IsLeader { get; set; }
    public bool WillBeLeader { get; set; }

    public delegate void LeaderCatTypeChanged(CatType type);
    public static event LeaderCatTypeChanged OnLeaderCatTypeChanged;

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
        spawnPosition = transform.position;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (WillBeLeader) {
            IsLeader = true;
            WillBeLeader = false;
            OnLeaderCatTypeChanged?.Invoke(type);
        }

        var speed = (tileSize / moveCooldownTime) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
        isMoving = transform.position != targetPosition;

        if (!IsLeader)
            return;

        // Inter-Cat Collision
        var scatterCollisions = Physics2D.OverlapCircleAll(targetPosition, 0.1f, playerMask);
        foreach (var collision in scatterCollisions) {
            var scatterCat = collision.GetComponent<CongaCat>();
            if (scatterCat != this && !scatterCat.isMoving) {
                var scatterCatLeader = FindLeaderOf(scatterCat);
                scatterCatLeader.follower = null;
                scatterCat.ScatterCats();
            }
        }

        // Cat Pickup
        var pickup = Physics2D.OverlapCircle(targetPosition, 0.1f, catPickupMask);
        if (pickup) {
            var pickupCat = pickup.GetComponent<CongaCat>();
            if (!pickupCat.isMoving) {
                var last = FindLast();
                last.follower = pickupCat;
                pickupCat.targetPosition = last.previousPosition;
                pickupCat.gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }
    }

    void OnPlayerMoved(Vector2 newPosition)
    {
        if (IsLeader) {
            if (newPosition == (Vector2)previousPosition && follower) {
                follower.ScatterCats();
                follower = null;
            }

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

    CongaCat FindLeaderOf(CongaCat cat)
    {
        if (follower == cat)
            return this;

        return follower.FindLeaderOf(cat);
    }

    void ScatterCats()
    {
        targetPosition = spawnPosition;
        isMoving = true;
        gameObject.layer = LayerMask.NameToLayer("CatPickup");

        if (follower)
            follower.ScatterCats();

        follower = null;
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
        isMoving = true;
    }

    public void OnLevelFinished()
    {
        IsLeader = false;
    }
}
