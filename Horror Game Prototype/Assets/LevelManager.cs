using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string level2DName = "2D Game";

    string currentLevel;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            LoadInTV(level2DName);
    }

    public void LoadInTV(string levelName)
    {
        if (currentLevel != null)
            SceneManager.UnloadSceneAsync(currentLevel);
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        currentLevel = levelName;
    }
}
