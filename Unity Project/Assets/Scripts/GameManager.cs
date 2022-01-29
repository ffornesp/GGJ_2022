using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static bool is_playing = false;

	private float _timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
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
        	_timer = 0f;
        }
    }
}
