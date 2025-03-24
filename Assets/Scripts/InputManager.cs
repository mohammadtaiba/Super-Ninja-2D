using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Public variables
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode attackKey = KeyCode.Mouse0;

    // Private variables
    private float horizontalMove;

    // Update is called once per frame
    void Update()
    {
        // Get horizontal movement input
        horizontalMove = Input.GetAxisRaw("Horizontal");
    }

    public float GetHorizontalMove()
    {
        return horizontalMove;
    }

    public bool GetJumpInput()
    {
        return Input.GetKeyDown(jumpKey);
    }

    public bool GetAttackInput()
    {
        return Input.GetKeyDown(attackKey);
    }
}
