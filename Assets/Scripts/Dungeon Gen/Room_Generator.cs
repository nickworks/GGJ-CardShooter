using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Generator : MonoBehaviour
{
    public GameObject nDoor;
    public GameObject sDoor;
    public GameObject eDoor;
    public GameObject wDoor;

    public DoorSensor nSensor;
    public DoorSensor sSensor;
    public DoorSensor eSensor;
    public DoorSensor wSensor;

    public List<GameObject> rooms;
    public GameObject hiddenWalls;

    // Start is called before the first frame update
    void Start()
    {
        equipSensors();

        if(transform.position != Vector3.zero)
        {
            GameObject room = Instantiate(rooms[(int)Random.Range(0, rooms.Count)]);
            
            room.transform.position = transform.position;
        }
        
    }

    void equipSensors()
    {
        nSensor.door = nDoor;
        sSensor.door = sDoor;
        eSensor.door = eDoor;
        wSensor.door = wDoor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
