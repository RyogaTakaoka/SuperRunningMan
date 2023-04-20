using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public AudioClip brokenSE;
    private GameObject player;
    private PlayerController playerCtl;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCtl = player.GetComponent<PlayerController>();
        this.source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Attack" && playerCtl.status == 1)
        {
            AudioSource.PlayClipAtPoint(brokenSE, transform.position);
            this.gameObject.SetActive(false);
        }
    }
}
