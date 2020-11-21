using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject menu;
    public GameObject gameOver;
    public Player_1 pl_1;
    public Player_2 pl_2;
    public Text point1;
    public Text point2;
    private string win;
    public Text WinName;
    public Text HP1;
    public Text PN1;
    public Text HP2;
    public Text PN2;
    public bool gmover;
    public AudioSource gameMusic;
    public AudioSource winMusic;
    public AudioSource menuMusic;


    private void Start()
    {
        
        gmover = false;
        Time.timeScale = 1;
    }

    #region Методы кнопок
    public void Menu()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
        gameMusic.Stop();
        Cursor.visible = true;
        
    }

    public void StartGame()
    {
        
        Time.timeScale = 1;
        menu.SetActive(false);
        Cursor.visible = false;
        menuMusic.Stop();
        gameMusic.Play();
    }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion
    #region Условие победы

    private void WinPlayer()
    {
        if(pl_2.hp <=0 || pl_1.point >= 10)
        {
            
            gameOver.SetActive(true);
            win = "Player 1";
            gmover = true;
        }

        else if(pl_1.hp <= 0 || pl_2.point >= 10)
        {
            
            gameOver.SetActive(true);
            win = "Player 2";
            gmover = true;
        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu();
            menuMusic.Play();
            gameMusic.Stop();
            SceneManager.LoadScene(0);
        }

        WinPlayer();

        point1.text = pl_1.point + "";
        point2.text = pl_2.point + "";
        WinName.text = win;
        HP1.text = pl_1.hp + "";
        HP2.text = pl_2.hp + "";
        PN1.text = pl_1.point + "";
        PN2.text = pl_2.point + "";
        if (gmover)
        {
            Time.timeScale = 0.2f;
            gameMusic.Stop();
        }
            
    }
}
