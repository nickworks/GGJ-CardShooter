using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    Pawn pawn;


    Vector2 moveAxis = Vector2.zero;

    void Start()
    {
        pawn = GetComponent<Pawn>();
    }
    void Update() {
        pawn.Move(moveAxis);
    }

    /// <summary>
    /// This is called when the Move axis is... updated?
    /// </summary>
    /// <param name="ctxt"></param>
    public void OnMove(InputAction.CallbackContext ctxt) {
        moveAxis = ctxt.ReadValue<Vector2>();
    }
}
