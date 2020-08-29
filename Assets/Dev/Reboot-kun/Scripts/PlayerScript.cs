using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{   int inputDirX;
    int inputDirY;

    private void Update()
    {
        void GetInput(KeyCode plusKeyX, KeyCode minusKeyX, KeyCode plusKeyY, KeyCode minusKeyY)
        {
            if (Input.GetKeyDown(plusKeyX))
                inputDirX = 1;
            if (Input.GetKeyDown(minusKeyX))
                inputDirX = -1;

            if (Input.GetKey(minusKeyX) && Input.GetKeyUp(plusKeyX))
                inputDirX = -1;
            if (Input.GetKey(plusKeyX) && Input.GetKeyUp(minusKeyX))
                inputDirX = 1;

            if (!Input.GetKey(plusKeyX) && !Input.GetKey(minusKeyX))
                inputDirX = 0;

            if (Input.GetKeyDown(plusKeyY))
                inputDirY = 1;
            if (Input.GetKeyDown(minusKeyY))
                inputDirY = -1;

            if (Input.GetKey(minusKeyY) && Input.GetKeyUp(plusKeyY))
                inputDirY = -1;
            if (Input.GetKey(plusKeyY) && Input.GetKeyUp(minusKeyY))
                inputDirY = 1;

            if (!Input.GetKey(plusKeyY) && !Input.GetKey(minusKeyY))
                inputDirY = 0;
        }
        GetInput(KeyCode.D, KeyCode.A, KeyCode.W, KeyCode.S);
        Debug.Log(inputDirX);
        Debug.Log(inputDirY);
    }
}
