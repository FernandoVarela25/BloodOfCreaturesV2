using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameover : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    private LeslieMovement movelesbi;
    public string mainmenu;
    private void Start()
    {
        movelesbi = GameObject.FindGameObjectWithTag("Leslie").GetComponent<LeslieMovement>();
        movelesbi.DeadPlayer += ActivateMenu;
    }
    private void ActivateMenu(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);

    }
    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainmenu);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
