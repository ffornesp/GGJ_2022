using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static bool is_playing = false;

	private float _timer = 0f;
	private GameObject _boosts;
    private GameObject _coins;
    // Start is called before the first frame update
    void Start()
    {
    	_boosts = GameObject.FindGameObjectWithTag("Boosts");
        _coins = GameObject.FindGameObjectWithTag("Coins");
    }

    // Update is called once per frame
    void Update()
    {
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
