using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	private	PlayerControl	_player1;
	private	PlayerControl	_player2;
	private GameObject      _camera;
    // Start is called before the first frame update
    void Start()
    {
    	GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    	foreach (GameObject player in players)
    		set_player(player.GetComponent<PlayerControl>());
    	_camera = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 _player1_position = _player1.GetCurrentPosition();
    	Vector3 _player2_position = _player2.GetCurrentPosition();
    	Vector3 _middle = (_player1_position + _player2_position) / 2;
    	float _distance = Vector3.Distance(_player1_position, _player2_position);

    	if (_distance < 10)
    		_camera.transform.Translate(Vector3.forward * Time.deltaTime);
    	else if (_distance > 10)
    		_camera.transform.Translate(Vector3.forward * Time.deltaTime * -1);
    }

    void	set_player(PlayerControl player)
    {
    	if (player.is_p1)
    		_player1 = player;
    	else
    		_player2 = player;
    }
}
