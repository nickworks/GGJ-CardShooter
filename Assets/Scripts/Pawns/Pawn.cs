using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Pawn : MonoBehaviour
{
    public float moveSpeed = 3;
    CharacterController body;

    void Start()
    {
        body = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        
    }

    #region API (call these funcs from controller classes):
    /// <summary>
    /// This function tells the pawn to move.
    /// </summary>
    /// <param name="dir">The direction to move the pawn. This should be a 2D value, as if viewing from above.</param>
    public void Move(Vector2 dir) {

        Vector3 dis = new Vector3(dir.x, 0, dir.y) * moveSpeed;

        body.SimpleMove(dis);
    }
    public void LookAim(float angle) {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    public void Attack() {

    }
    #endregion
}
