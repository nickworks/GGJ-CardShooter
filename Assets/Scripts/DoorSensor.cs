using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    bool needDoorSet = true;
    public GameObject door;

    private void OnTriggerStay(Collider other)
    {
        if(needDoorSet && door != null)
        {
            door.transform.localPosition = new Vector3(door.transform.localPosition.x, 6, door.transform.localPosition.y);
            needDoorSet = false;
        }
    }
}
