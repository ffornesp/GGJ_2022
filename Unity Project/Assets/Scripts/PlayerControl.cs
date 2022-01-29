using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool is_p1;

    private bool _grounded_player;
    private CharacterController _chara_control;
    private Vector3 _chara_velocity;

	private float _gravity = -50f;
    public float _speed = 8.0f;
	private float _jump_speed = 2.0f;
    private float _speed_multiplier = 1f;

    void Start()
    {
    	_chara_control = GetComponent<CharacterController>();
    	_chara_control.minMoveDistance = 0;
    }

    void Update()
    {
        player_movement();
    }

    void player_movement()
    {
        Vector3 move = Vector3.zero;

        _grounded_player = _chara_control.isGrounded;
        
        if (_grounded_player && _chara_velocity.y < 0)
            _chara_velocity.y = 0f;
        
        if (is_p1)
            move = new Vector3(Input.GetAxis("Horizontal_P1"), 0, Input.GetAxis("Vertical_P1"));
        else
            move = new Vector3(Input.GetAxis("Horizontal_P2"), 0, Input.GetAxis("Vertical_P2"));

        _chara_control.Move(move * Time.deltaTime * _speed * _speed_multiplier);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (!is_p1 && Input.GetKeyDown(KeyCode.Space) && _grounded_player)
            _chara_velocity.y += Mathf.Sqrt(_jump_speed * -3.0f * _gravity * _speed_multiplier);
        if (is_p1 && Input.GetKeyDown(KeyCode.RightShift) && _grounded_player)
            _chara_velocity.y += Mathf.Sqrt(_jump_speed * -3.0f * _gravity * _speed_multiplier);

        _chara_velocity.y +=  _gravity * Time.deltaTime;
        _chara_control.Move(_chara_velocity * Time.deltaTime);
    }

    public Vector3 GetCurrentPosition(){
        return (transform.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Slow_Trap")
            _speed_multiplier = 0.5f;
    }

    void    OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Slow_Trap")
            _speed_multiplier = 1f;
    }


}