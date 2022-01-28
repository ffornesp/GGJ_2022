using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	private CharacterController	_chara_control;
	private	Vector3	_chara_velocity;
    private bool groundedPlayer;

	private float gravityValue = -9.81f;
    private float playerSpeed = 2.0f;
	private float jumpHeight = 3.0f;

	public bool is_p1;
    // Start is called before the first frame update
    void Start()
    {
    	_chara_control = GetComponent<CharacterController>();
    	_chara_control.minMoveDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        player_movement();
    }

    void player_movement()
    {
    	//if (_chara_control.isGrounded && _chara_velocity.y < 0)
    		//_chara_velocity.y = 0f;

        groundedPlayer = _chara_control.isGrounded;
        if (groundedPlayer && _chara_velocity.y < 0)
        {
            _chara_velocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal_P1"), 0, Input.GetAxis("Vertical_P1"));
        _chara_control.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            _chara_velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        _chara_velocity.y += gravityValue * Time.deltaTime;
        _chara_control.Move(_chara_velocity * Time.deltaTime);
    

        /*
    	Vector3 move;

    	if (is_p1)
    		move = new Vector3(Input.GetAxis("Horizontal_P1"), 0, Input.GetAxis("Vertical_P1"));
		else
			move = new Vector3(Input.GetAxis("Horizontal_P2"), 0, Input.GetAxis("Vertical_P2"));


        Vector3 _velocity.y += gravityValue * Time.deltaTime;
        _chara_control.Move(move * Time.deltaTime);

        if (move != Vector3.zero)
            transform.forward = move;

        
		if (Input.GetKeyDown(KeyCode.Space) && _chara_control.isGrounded)
        {
            move.y = 50f;
            Debug.Log("Works");
        }
		move.y -= gravityValue * 5 * Time.deltaTime;
		//transform.rotation = Quaternion.Euler(0, transform.rotation.y, move.z);
        _chara_control.Move(move * Time.deltaTime * m_Speed);

        if (move != Vector3.zero)
        {
        	Vector3 rota;
        	rota = move;
        	rota.x = 0;
        	rota.z = 0;

        	Vector3	test;
        	test = new Vector3(0, transform.rotation.y, 0);
            gameObject.transform.forward = Vector3.Lerp(test, rota, 5);
        }
        // Changes the height position of the player..
        Debug.Log(_chara_control.isGrounded);
        */
    }
}
