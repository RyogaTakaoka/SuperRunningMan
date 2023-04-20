using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllosaurusController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerCtl;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCtl = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Attack")
        {
            switch (playerCtl.status)
            {
                case 1:
                    Destroy(this.gameObject);
                    break;
                case 0:
                case -1:
                    break;
            }
        }
    }
}
