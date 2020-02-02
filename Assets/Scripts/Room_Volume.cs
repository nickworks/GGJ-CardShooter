using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Volume : MonoBehaviour
{
    public float id = 0;
    private void Start()
    {
        id = Random.Range(0, 255);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Pawn pawn = collider.GetComponent<Pawn>();

        if(pawn != null)
        {
            pawn.currentRoom = this;
        }
    }
    
}
