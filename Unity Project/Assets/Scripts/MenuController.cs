using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public GameObject	menu;
	public GameObject	tutorial;
	public GameObject	credits;

	void Start()
	{
		menu.SetActive(true);
		tutorial.SetActive(false);
		credits.SetActive(false);
	}

	public void load_tutorial()
	{
		menu.SetActive(false);
		tutorial.SetActive(true);
	}

    public void load_credits()
    {
    	menu.SetActive(false);
    	credits.SetActive(true);
    }

    public void go_back(int i)
    {
    	if (i == 0)
    		tutorial.SetActive(false);
    	else if (i == 1)
    		credits.SetActive(false);
    	menu.SetActive(true);
    }

    public void load_game()
    {
    	SceneManager.LoadScene(1);
    }

    public void	exit_game()
    {
    	Application.Quit();
    }
}
