using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    /// <summary>
    /// This is the "pawn" currently "possessed" by this controller.
    /// Currently, the pawn is just a script also loaded onto this same game object.
    /// </summary>
    Pawn pawn;

    Camera cam;

    float lookDirection = 90;
    Vector2 moveAxis = Vector2.zero;

    void Start()
    {
        pawn = GetComponent<Pawn>();
        cam = Camera.main;
    }
    void Update() {
        pawn.Move(moveAxis);
        pawn.LookAim(lookDirection);
    }

    /// <summary>
    /// This is called when the Move axis is... updated?
    /// </summary>
    /// <param name="ctxt"></param>
    public void OnMove(InputAction.CallbackContext ctxt) {
        moveAxis = ctxt.ReadValue<Vector2>();
    }
    public void OnLookStick(InputAction.CallbackContext ctxt) {
        Vector2 look = ctxt.ReadValue<Vector2>();
        lookDirection = Mathf.Rad2Deg * Mathf.Atan2(look.x, look.y);
    }
    public void OnLookMouse(InputAction.CallbackContext ctxt) {
        if (cam == null) return;

        Vector3 mouse = ctxt.ReadValue<Vector2>();
        
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = cam.ScreenPointToRay(mouse);

        if (plane.Raycast(ray, out float d)) {
            Vector3 dis = ray.GetPoint(d) - transform.position;
            lookDirection = Mathf.Rad2Deg * Mathf.Atan2(dis.x, dis.z);
        }
    }
}
