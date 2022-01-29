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
    public Sprite[] countdown_sprites;
    public Image    countdown_img;

	private float _timer = 0f;
    private float _countdown_timer = 0;
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

        countdown_img.transform.parent.gameObject.SetActive(true);
        _countdown_timer += Time.deltaTime;
        if (_countdown_timer < 1f)
            countdown_img.sprite = countdown_sprites[0];
        else if (_countdown_timer > 1f && _countdown_timer < 2f)
            countdown_img.sprite = countdown_sprites[1];
        else if (_countdown_timer > 2f && _countdown_timer < 3f)
            countdown_img.sprite = countdown_sprites[2];
        else    
            countdown_img.transform.parent.gameObject.SetActive(false);

        if (_timer > 2.9f && _timer < 3.1f)
        {	
        	Debug.Log("PLAY!");
        	is_playing = true;
        }
        if (_timer > 3.2f && !is_playing)
        {
        	reset_collectables(_boosts);
        	reset_collectables(_coins);
        	_timer = 0f;
            _countdown_timer = 0;
        }
        Win_condition();
    }

    void countdown()
    {

    }

    void Win_condition()
    {
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
