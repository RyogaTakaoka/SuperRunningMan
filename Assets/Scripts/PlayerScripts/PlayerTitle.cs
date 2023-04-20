using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTitle : MonoBehaviour
{
    TitleManager tm;
    PlayerController pCtl;

    // Start is called before the first frame update
    void Start()
    {
        tm = FindObjectOfType<TitleManager>();
        pCtl = GetComponent<PlayerController>();
        pCtl.isClear = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x > 25)
        {
            this.transform.position = new Vector2(-25, -3.65f);
            pCtl.status = 0;
            tm.setMeat = true;
        }
    }
}
