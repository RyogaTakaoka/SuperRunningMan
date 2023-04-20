using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip gameOverSE;
    [SerializeField] AudioClip gameClearSE;
    [SerializeField] GameObject gameClear;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject continueBtn,backBtn;
    Button firstSelect;
    AudioSource audioSource;
    FadeScene fs;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        fs = FindObjectOfType<FadeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameClear()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(gameClearSE);
        gameClear.SetActive(true);
        Invoke("BackToSelect", 7.0f);
    }

    public void GameOver()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(gameOverSE);
        gameOver.SetActive(true);
        continueBtn.SetActive(true);
        backBtn.SetActive(true);
        firstSelect = GameObject.Find("Canvas/Continue").GetComponent<Button>();
        firstSelect.Select();
    }

    public void Continue()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        fs.FadeLoadScene(thisScene.name);
    }

    public void BackToSelect()
    {
        fs.FadeLoadScene("StageSelect");
    }
}
