﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : SingletonMonoBehaviour<QuitGame>
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
