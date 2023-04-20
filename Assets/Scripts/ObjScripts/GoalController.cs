using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public int stageNum;
    private GameObject mainCamera;
    private CameraController cameraCtl;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.x - mainCamera.transform.position.x <= 15)
        {
            cameraCtl = mainCamera.GetComponent<CameraController>();
            cameraCtl.speed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            cameraCtl = mainCamera.GetComponent<CameraController>();
            cameraCtl.speed = 0;
        }

        if (col.gameObject.tag == "Player")
        {
            if(PlayerPrefs.GetInt("ClearedStage", 0) <= stageNum)
            {
                PlayerPrefs.SetInt("ClearedStage", stageNum + 1);
                //PlayerPrefsをセーブする         
                PlayerPrefs.Save();
            }
        }
    }
}
