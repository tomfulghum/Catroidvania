using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour
{
    [SerializeField] LayerMask pushingMask;
    [SerializeField] float tileSize = 0.5f;
    [SerializeField] float moveSpeed = 2f;

    Vector3 targetPosition;
    bool isMoving;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        isMoving = transform.position != targetPosition;

        if (isMoving)
            return;

        var pushingCollider = Physics2D.OverlapCircle(transform.position, .2f, pushingMask);
        if (pushingCollider) {
            var pushCat = pushingCollider.GetComponent<CongaCat>();
            if (pushCat.Type == CatType.Push && pushCat.IsLeader) {
                Vector3 direction = ((Vector2)(transform.position - pushingCollider.transform.position)).normalized * tileSize;
                targetPosition = transform.position + direction;
            }
        }
    }
}
