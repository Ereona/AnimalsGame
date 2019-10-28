using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject PlayMenu;

    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
    }

    public void ShowPlayMenu()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(true);
    }
}
