using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static bool is_playing = false;
    public Text p1_coin_text, p2_coin_text;
    public static int p1_coin_count, p2_coin_count;

	private float _timer = 0f;
	private GameObject _boosts;
    private GameObject _coins;
    private int _win_coin_amount = 6;
    // Start is called before the first frame update
    void Start()
    {
    	_boosts = GameObject.FindGameObjectWithTag("Boosts");
        _coins = GameObject.FindGameObjectWithTag("Coins");
        p1_coin_count = 0;
        p2_coin_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        p1_coin_text.text = " Player 1 coins: " + p1_coin_count;
        p2_coin_text.text = " Player 2 coins: " + p2_coin_count;
        _timer += Time.deltaTime;
        if (_timer > 2f && _timer < 2.5f)
        {	
        	Debug.Log("PLAY!");
        	is_playing = true;
        }
        if (_timer > 2.5f && !is_playing)
        {
        	reset_collectables(_boosts);
        	reset_collectables(_coins);
        	_timer = 0f;
        }
        if (p1_coin_count >= _win_coin_amount)
        {
            Debug.Log("Player one wins");
            SceneManager.LoadScene(0);
        }
        else if (p2_coin_count >= _win_coin_amount)
        {
            Debug.Log("Player two wins");
            SceneManager.LoadScene(0);
        }
    }

    void reset_collectables(GameObject obj_parent){
    	int	i = 0;
    	Debug.Log(obj_parent.transform.childCount);

    	while (i < obj_parent.transform.childCount)
    	{
   			obj_parent.transform.GetChild(i).gameObject.SetActive(true);
   			i++;
    	}
    }
}
