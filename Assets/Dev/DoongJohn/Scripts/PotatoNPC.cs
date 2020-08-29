using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoNPC : MonoBehaviour
{
    MapManager mapManager;
    Rigidbody2D rb2D;
    Transform player;
    [SerializeField] float moveSpeed;

    List<Node> movePath = new List<Node>();


    private void Awake()
    {
        mapManager = GameObject.Find("PotatoMapManager").GetComponent<MapManager>();
        rb2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerScript>().transform;
    }
    private void FixedUpdate()
    {
        Move();
    }

    (int x, int y) GetGridPos(Transform tf)
    {
        (int x, int y) gridPos;
        gridPos.x = Mathf.FloorToInt(mapManager.mapHalfSize.x + tf.position.x);
        gridPos.y = Mathf.FloorToInt(mapManager.mapHalfSize.y + tf.position.y);
        return gridPos;
    }
    Vector2 GetWorldPos(Vector2 gridPos)
    {
        Vector2 wroldPos;
        wroldPos.x = gridPos.x - mapManager.mapHalfSize.x + 0.5f;
        wroldPos.y = gridPos.y - mapManager.mapHalfSize.y + 0.5f;
        return wroldPos;
    }
    void Move()
    {
        var (curX, curY) = GetGridPos(transform);

        var (targetX, targetY) = GetGridPos(player);
        //var (targetX, targetY) = (4, 4);

        Vector2 targetWorldPos;
        float dist = float.PositiveInfinity;

        movePath = AStar.Search(mapManager.map, mapManager.map.Grid[curX, curY], mapManager.map.Grid[targetX, targetY]);

        if (movePath.Count == 0 && dist == 0f)
        {
            rb2D.velocity = Vector2.zero;
            return;
        }

        if (movePath.Count != 0)
            targetWorldPos = GetWorldPos(movePath[0].Position);
        else
            targetWorldPos = GetWorldPos(new Vector2(targetX, targetY));

        dist = Vector2.Distance(targetWorldPos, transform.position);

        Vector2 moveDir = (targetWorldPos - (Vector2)transform.position).normalized;
        rb2D.velocity = moveDir * Mathf.Min(dist / Time.fixedDeltaTime, moveSpeed);
    }
}
