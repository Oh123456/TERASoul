using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void MovetoScene()
    {
        SceneManager.LoadScene("Map1");
    }

    public void EscapeGame()
    {
        Application.Quit();
    }
}
