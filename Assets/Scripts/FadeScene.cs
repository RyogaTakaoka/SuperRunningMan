using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScene : SingletonMonoBehaviour<FadeScene>
{
    Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        fade = FindObjectOfType<Fade>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeLoadScene(string sceneName)
    {
        fade.FadeIn(0.7f, () =>
        {
            SceneManager.LoadScene(sceneName);
            fade.FadeOut(0.7f);
        });
        
    }
}
