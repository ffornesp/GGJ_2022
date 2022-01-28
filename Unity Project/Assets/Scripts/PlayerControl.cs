using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	private CharacterController	_chara_control;
	private	Vector3	_chara_velocity;
	private float gravityValue = 9.81f;
	private float jumpHeight = 5.0f;
	float m_Speed;
	public bool is_p1;
    // Start is called before the first frame update
    void Start()
    {
    	_chara_control = GetComponent<CharacterController>();
    	m_Speed = 5.0f;
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
    	Vector3 move;

    	if (is_p1)
    		move = new Vector3(Input.GetAxis("Horizontal_P1"), 0, Input.GetAxis("Vertical_P1"));
		else
			move = new Vector3(Input.GetAxis("Horizontal_P2"), 0, Input.GetAxis("Vertical_P2"));

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
        
    }
}
