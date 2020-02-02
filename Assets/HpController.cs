using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpController : MonoBehaviour
{

    Pawn player;
    public GameObject hpBar;

    // Start is called before the first frame update
    void Start()
    {
        findPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            findPlayer();
            return;
        }

        if(player.health/100 != hpBar.transform.localScale.x)
        {
            hpBar.transform.localScale = new Vector3(player.health / 100, 1, 1);
        }
    }

    void findPlayer()
    {
        player = GameObject.FindObjectOfType<PlayerController>().GetPawn();
    }
}
