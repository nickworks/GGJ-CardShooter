using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    bool needDoorSet = true;
    public GameObject door;
    public float offset = 6;

    private void OnTriggerStay(Collider other)
    {
        if(needDoorSet && door != null)
        {
            door.transform.localPosition = new Vector3(door.transform.localPosition.x, offset, door.transform.localPosition.y);
            needDoorSet = false;
        }
    }
}
