using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBehavior : MonoBehaviour
{
    public float speed = 2f;
    public float rayDistance = 0.5f;
    public LayerMask floorLayer;

    public int enemyColor = 0;

    private Rigidbody2D rb;
    private bool face = true;

    [SerializeField]private Transform check;

    [SerializeField]private Transform monsterHead;
    public float monsterHeadPosition = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        monsterHead.transform.position = new Vector2(transform.position.x, transform.position.y + monsterHeadPosition);
        Patrol();
        DetectWallOrEdge();
    }

    void Patrol()
    {
        float moveDirection = face ? 1 : -1;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
    }

    void DetectWallOrEdge()
    {
        Vector2 direction = face ? Vector2.right : Vector2.left;
        Vector2 downDirection = Vector2.down;

        Vector2 rayOrigin = check.position + new Vector3(direction.x * rayDistance, 0, 0);

        RaycastHit2D wallHit = Physics2D.Raycast(rayOrigin, direction, rayDistance, floorLayer);

        RaycastHit2D groundHit = Physics2D.Raycast(rayOrigin, downDirection, rayDistance, floorLayer);

        Debug.DrawRay(rayOrigin, direction * rayDistance, Color.red);
        Debug.DrawRay(rayOrigin, downDirection * rayDistance, Color.blue);

        if (wallHit || !groundHit)
        {
            Flip();
        }
    }

    void Flip()
    {
        face = !face;

        Vector3 localScale = transform.localScale;

        localScale.x *= -1;

        transform.localScale = localScale;
    }
}
