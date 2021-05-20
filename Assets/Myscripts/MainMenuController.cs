using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject play;
    public GameObject quit;
    public GameObject controls;
    public GameObject lore;
   

    public void ChangeMenu()
    {
        play.SetActive(false);
        quit.SetActive(false);
        controls.SetActive(false);
        lore.SetActive(true);
    }
    public void Lore()
    {
        lore.SetActive(false);
        controls.SetActive(true);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
