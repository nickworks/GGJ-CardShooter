using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller {
    /// <summary>
    /// This is the "pawn" currently "possessed" by this controller.
    /// Currently, the pawn is just a script also loaded onto this same game object.
    /// </summary>
    Pawn pawn;

    /// <summary>
    /// This reference to a camera is used for raycasting when using the mouse to aim.
    /// </summary>
    Camera cam;

    /// <summary>
    /// This stores the direction that the player is trying to move.
    /// We store it so that we can then pass it to the pawn every Update().
    /// </summary>
    Vector2 moveAxis = Vector2.zero;

    /// <summary>
    /// The script that controls the GUI.
    /// </summary>
    CardManager gui;

    void Start() {
        pawn = GetComponent<Pawn>();
        cam = Camera.main;
        gui = GameObject.FindObjectOfType<CardManager>();

        pawn.PickupTome(Tome.Random());
        pawn.PickupTome(Tome.Random());
        pawn.PickupTome(Tome.Random());

        UpdateHUD();
    }
    /// <summary>
    /// Tick
    /// </summary>
    void Update() {
        if (Game.isPaused) return;
        pawn.Move(moveAxis);

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            pawn.PickupCard(Card.Random());
        }
        //if (Input.GetKeyDown(KeyCode.KeypadMinus)) PopCard();

    }

    /// <summary>
    /// This is called when the Move axis is updated.
    /// Store the input and pass to pawn each Update().
    /// </summary>
    /// <param name="ctxt"></param>
    public void OnMove(InputAction.CallbackContext ctxt) {
        moveAxis = ctxt.ReadValue<Vector2>();
    }
    /// <summary>
    /// When the fire action is trigger, tell the pawn to attack.
    /// </summary>
    /// <param name="ctxt"></param>
    public void OnFire(InputAction.CallbackContext ctxt) {
        if (Game.isPaused) return;
        if (ctxt.phase == InputActionPhase.Started) pawn.Attack();
    }
    /// <summary>
    /// When aiming with a controller stick,
    /// calculate the angle and tell the pawn where to aim.
    /// </summary>
    /// <param name="ctxt"></param>
    public void OnLookStick(InputAction.CallbackContext ctxt) {
        if (Game.isPaused) return;
        Vector2 look = ctxt.ReadValue<Vector2>();
        float threshold = 0.5f;
        if (look.sqrMagnitude > threshold * threshold) {
            float lookYaw = Mathf.Rad2Deg * Mathf.Atan2(look.x, look.y);
            pawn.LookAim(lookYaw);
        }
    }
    /// <summary>
    /// When the mouse is moved, raycast from the camera,
    /// and tell pawn to aim where the ray hits.
    /// </summary>
    /// <param name="ctxt"></param>
    public void OnLookMouse(InputAction.CallbackContext ctxt) {
        if (Game.isPaused) return;
        if (cam == null) return;

        Vector3 mouse = ctxt.ReadValue<Vector2>();

        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = cam.ScreenPointToRay(mouse);

        if (plane.Raycast(ray, out float d)) {
            Vector3 dis = ray.GetPoint(d) - transform.position;
            float lookYaw = Mathf.Rad2Deg * Mathf.Atan2(dis.x, dis.z);
            pawn.LookAim(lookYaw);
        }
    }

    public Pawn GetPawn()
    {
        return pawn;
    }

    public void OnNextTome(InputAction.CallbackContext ctxt) {
        if (ctxt.phase != InputActionPhase.Started) return;
        pawn.NextTome();
        UpdateHUD(1);
    }
    public void OnPrevTome(InputAction.CallbackContext ctxt) {
        if (ctxt.phase != InputActionPhase.Started) return;
        pawn.PrevTome();
        UpdateHUD(-1);
    }
    void UpdateHUD(int shift = 0) {
        gui.SwitchTomes(pawn.tomes, pawn.CurrentTome(), shift);
    }
}
