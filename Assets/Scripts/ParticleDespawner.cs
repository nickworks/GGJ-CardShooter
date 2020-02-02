using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDespawner : MonoBehaviour
{
    float lifeTime = 5;

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
