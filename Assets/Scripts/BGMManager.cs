using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : SingletonMonoBehaviour<BGMManager>
{
    public AudioClip BGM;
    private AudioSource source;
    private string beforeScene = "Title";

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
        source.clip = BGM;
        source.Play();
        //シーンが切り替わった時に呼ばれるメソッドを登録
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    //シーンが切り替わった時に呼ばれるメソッド
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        //シーンの変化で判定
        if (nextScene.name != "Title" && nextScene.name != "StageSelect")
        {
            source.Stop();
        }

        if (beforeScene != "Title" && nextScene.name == "StageSelect")
        {
            source.Play();
        }

        //遷移後のシーン名を「１つ前のシーン名」として保持
        beforeScene = nextScene.name;
    }
}
