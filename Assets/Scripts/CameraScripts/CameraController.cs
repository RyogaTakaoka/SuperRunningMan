using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10;
    private GameObject player;
    private PlayerController playerCtl;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerCtl = player.GetComponent<PlayerController>();
        this.rb = GetComponent<Rigidbody>();
        this.transform.position = new Vector3(player.transform.position.x + 5,0,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //強制横スクロール
        rb.velocity = new Vector3(speed, 0, 0);

        //Playerが画面外になったときの処理
        float def = this.transform.position.x - player.transform.position.x;
        if(def > 18)
        {
            playerCtl.Dead();         
        }

        if (playerCtl.isDead)
        {
            CameraStop();
        }
    }

    private void CameraStop()
    {
        if (speed < 0)
        {
            speed = 0;
        }
        else
        {
            speed -= 0.1f;
        }
    }
}
