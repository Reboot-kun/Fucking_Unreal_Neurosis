using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb2D;
    Vector2 inputDir;
    Vector2 dashDir;
    [SerializeField] float moveSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDist;
    float dashTime;
    bool isDashing = false;
    bool canWalk = true;
    bool canShoot = true;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputDir.x = GetInput(inputDir.x, KeyCode.D, KeyCode.A);
        inputDir.y = GetInput(inputDir.y, KeyCode.W, KeyCode.S);
        if (!isDashing)
            GetDashInput(KeyCode.D, KeyCode.A, KeyCode.W, KeyCode.S);
        if (Input.GetKeyDown(KeyCode.LeftShift) && (dashDir.x != 0 || dashDir.y != 0))
        {
            isDashing = true;
            canWalk = false;
        }
    }

    private void FixedUpdate()
    {
        if (canWalk)
            Walk();
        if (isDashing)
            Dash();
    }

    float GetInput(float input, KeyCode plusKey, KeyCode minusKey)
    {
        if (Input.GetKeyDown(plusKey))
            input = 1;
        if (Input.GetKeyDown(minusKey))
            input = -1;

        if (Input.GetKey(minusKey) && Input.GetKeyUp(plusKey))
            input = -1;
        if (Input.GetKey(plusKey) && Input.GetKeyUp(minusKey))
            input = 1;

        if (!Input.GetKey(plusKey) && !Input.GetKey(minusKey))
            input = 0;

        if (!canWalk)
            input = 0;

        return input;
    }

    void GetDashInput(KeyCode plusKeyX, KeyCode minusKeyX, KeyCode plusKeyY, KeyCode minusKeyY)
    {

        if (Input.GetKeyDown(plusKeyX))
            dashDir.x = 1;
        if (Input.GetKeyDown(minusKeyX))
            dashDir.x = -1;
        if (Input.GetKey(plusKeyX) && Input.GetKey(minusKeyX))
            dashDir.x = 0;

        if (Input.GetKeyDown(plusKeyY))
            dashDir.y = 1;
        if (Input.GetKeyDown(minusKeyY))
            dashDir.y = -1;
        if (Input.GetKey(plusKeyY) && Input.GetKey(minusKeyY))
            dashDir.y = 0;
    }

    void Walk()
    {
        rb2D.velocity = inputDir.normalized * moveSpeed;
    }

    void Dash()
    {
        float dashTimeTotal = dashDist / dashSpeed;
        Collider2D[] dashHitBox = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        for (int i = 0; i < dashHitBox.Length; i++)
        {
            if (dashHitBox[i].gameObject == gameObject)
                continue;


            Rigidbody2D npcRb2D = dashHitBox[i].gameObject.GetComponent<Rigidbody2D>();
            if (npcRb2D == null)
                continue;

            //npcRb2D.velocity = asdasd
        }

        dashTime += Time.deltaTime;
        if (dashTime >= dashTimeTotal)
        {
            isDashing = false;
            dashTime = 0;
            canWalk = true;
            canShoot = true;
        }

        rb2D.velocity = dashDir * dashSpeed;
    }
}
