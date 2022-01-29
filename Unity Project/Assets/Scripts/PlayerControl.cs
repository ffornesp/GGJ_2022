using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	private CharacterController	_chara_control;

	private	Vector3	_chara_velocity;
    private bool grounded_Player;
	private float gravityValue = -50f;
    private float playerSpeed = 8.0f;
	private float jumpHeight = 2.0f;

	public bool is_p1;

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

        grounded_Player = _chara_control.isGrounded;
        
        if (grounded_Player && _chara_velocity.y < 0)
            _chara_velocity.y = 0f;
        
        if (is_p1)
            move = new Vector3(Input.GetAxis("Horizontal_P1"), 0, Input.GetAxis("Vertical_P1"));
        else
            move = new Vector3(Input.GetAxis("Horizontal_P2"), 0, Input.GetAxis("Vertical_P2"));

        _chara_control.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (!is_p1 && Input.GetKeyDown(KeyCode.Space) && grounded_Player)
            _chara_velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        if (is_p1 && Input.GetKeyDown(KeyCode.RightShift) && grounded_Player)
            _chara_velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        _chara_velocity.y +=  gravityValue * Time.deltaTime;
        _chara_control.Move(_chara_velocity * Time.deltaTime);
        
    }
}