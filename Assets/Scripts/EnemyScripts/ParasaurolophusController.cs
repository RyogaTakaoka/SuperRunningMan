using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasaurolophusController : MonoBehaviour
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
                case 0:
                    Destroy(this.gameObject);
                    break;
                case -1:
                    break;
            }
        }

        if (col.gameObject.tag == "Player")
        {
            playerCtl.rb.velocity = new Vector2(playerCtl.rb.velocity.x, 0);
            playerCtl.rb.AddForce(Vector2.up * playerCtl.jumpForce);
            switch (playerCtl.status)
            {
                case 1:
                case 0:
                    Destroy(this.gameObject);
                    break;
                case -1:
                    break;
            }
        }
    }
}
