using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	private	PlayerControl	_player1;
	private	PlayerControl	_player2;
	private GameObject      _camera;
	private float _previous_distance;

	public	float			distance_cam_target;
	public	GameObject 		camera_target;
    // Start is called before the first frame update
    void Start()
    {
    	GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    	foreach (GameObject player in players)
    		set_player(player.GetComponent<PlayerControl>());
    	_camera = transform.GetChild(0).gameObject;
    	_previous_distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 _player1_position = _player1.GetCurrentPosition();
    	Vector3 _player2_position = _player2.GetCurrentPosition();
    	Vector3 _middle = (_player1_position + _player2_position) / 2;
    	float _distance = Vector3.Distance(_player1_position, _player2_position);

    	bool _is_zooming = _distance >= _previous_distance ? false : true;

    	camera_target.transform.position = _middle;
    	distance_cam_target = Vector3.Distance(_camera.transform.position, _middle);

    	transform.position = new Vector3(_middle.x, 15f, _middle.z - 5f);

    	if (_distance > 15 && _distance < 60){
    		if (_distance - _previous_distance != 0){
    			if (!_is_zooming && distance_cam_target < 50)
	    				_camera.transform.Translate(Vector3.forward * -8f * Time.deltaTime);
    			if (_is_zooming && distance_cam_target > 10)
    				_camera.transform.Translate(Vector3.forward * 5f * Time.deltaTime);
    		}
    	}
    	else if (_distance <= 15f && distance_cam_target > 20f)
    		_camera.transform.Translate(Vector3.forward * 8f * Time.deltaTime);

    	_previous_distance = _distance;
    }

    void	set_player(PlayerControl player)
    {
    	if (player.is_p1)
    		_player1 = player;
    	else
    		_player2 = player;
    }
}
