using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearedStage : MonoBehaviour
{
    public int stageNum;
    public GameObject stage2;
    public GameObject stage3;
    public GameObject stage2Rock;
    public GameObject stage3Rock;
    public GameObject clear1;
    public GameObject clear2;
    public GameObject clear3;

    // Use this for initialization
    void Start()
    {
        //現在のstageNumを呼び出す
        stageNum = PlayerPrefs.GetInt("ClearedStage", 0);

        if (stageNum >= 2)
        {
            clear1.SetActive(true);
            stage2.SetActive(true);
            stage2Rock.SetActive(false);
        }

        if (stageNum >= 3)
        {
            clear2.SetActive(true);
            stage3.SetActive(true);
            stage3Rock.SetActive(false);
        }

        if (stageNum >= 4)
        {
            clear3.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}