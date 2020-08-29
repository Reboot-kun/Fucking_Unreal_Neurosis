using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb2D;
    Vector2 inputDir;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        inputDir.x = GetInput(inputDir.x, KeyCode.D, KeyCode.A);
        inputDir.y = GetInput(inputDir.y, KeyCode.W, KeyCode.S);
    }
    private void FixedUpdate()
    {
        Move();
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

        return input;
    }

    void Move()
    {
        rb2D.velocity = inputDir.normalized * 10f;
    }
}
