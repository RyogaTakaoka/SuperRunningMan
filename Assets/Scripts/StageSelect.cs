using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public GameObject manualPanel;
    public Button manualBack;
    Button firstSelect;
    FadeScene fs;

    // Start is called before the first frame update
    void Start()
    {
        firstSelect = GameObject.Find("Canvas/STAGE1").GetComponent<Button>();
        firstSelect.Select();
        fs = FindObjectOfType<FadeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            fs.FadeLoadScene("Title");
        }else if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            manualPanel.SetActive(true);
            manualBack.Select();
        }
    }

    public void LoadStage(int stageNum)
    {
        fs.FadeLoadScene("Stage"+stageNum.ToString());
    }

    public void Close()
    {
        manualPanel.SetActive(false);
        firstSelect.Select();
    }
}
