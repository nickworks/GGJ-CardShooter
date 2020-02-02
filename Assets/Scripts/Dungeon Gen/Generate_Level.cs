using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate_Level : MonoBehaviour
{
    public List<GameObject> levels;
    // Start is called before the first frame update
    void Start()
    {
        GameObject level = Instantiate(levels[(int) Random.Range(0, levels.Count)]);

        level.transform.position = Vector3.zero;
    }
    
}
