using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject[] meat;
    [System.NonSerialized] public bool setMeat = false;
    FadeScene fs;
    private int nextMeat = 1;

    void Start()
    {
        fs = FindObjectOfType<FadeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        //肉を配置し直す
        if(setMeat)
        {
            meat[nextMeat].SetActive(true);
            meat[nextMeat].transform.position = new Vector2(0, 10);
            nextMeat = 1 - nextMeat;
            setMeat = false;
        }
        
        //ステージセレクト画面に遷移
        if (Input.GetKeyDown(KeyCode.Return))
        {
            fs.FadeLoadScene("StageSelect");
        }
    }
}
