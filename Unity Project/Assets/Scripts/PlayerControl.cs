using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool is_p1;
    public bool is_runner;
    public Vector3   _init_pos;

    private bool _grounded_player;
    private CharacterController _chara_control;
    private Vector3 _chara_velocity;
    private GameObject _box_col;
	private float _gravity = -50f;
    public float _speed = 8.0f;
	private float _jump_speed = 2.0f;
    private float _speed_multiplier = 1f;
    private float _jump_multiplier = 1f;

    private Animator _animator;

    void Start()
    {
    	_chara_control = GetComponent<CharacterController>();
        _box_col = transform.GetChild(1).gameObject;
        _animator = transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetComponent<Animator>();
    	_chara_control.minMoveDistance = 0;
        _init_pos = transform.position;
        soft_reset();
    }

    void Update()
    {
        if (GameManager.is_playing)
            player_movement();
        else
        {
            _animator.SetBool("Is_moving", false);
            _animator.SetBool("Is_grounded", true);
        }
    }

    void player_movement()
    {
        Vector3 move = Vector3.zero;

        _grounded_player = _chara_control.isGrounded;
        if (_grounded_player)
            _animator.SetBool("Is_grounded", true);
        else
            _animator.SetBool("Is_grounded", false);

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

        if (move != Vector3.zero)
            _animator.SetBool("Is_moving", true);
        else
            _animator.SetBool("Is_moving", false);
        _chara_velocity.y +=  _gravity * Time.deltaTime;
        _chara_control.Move(_chara_velocity * Time.deltaTime * _jump_speed);
    }

    public Vector3 GetCurrentPosition(){
        return (transform.position);
    }

    void OnTriggerEnter(Collider col)
    {
        //Traps
        if (col.gameObject.tag == "Slow_Trap")
            _speed_multiplier *= 0.5f;


        //Boosts
        if (col.gameObject.tag == "Boost_Powerup"){
            _speed_multiplier *= 1.235f;
            col.gameObject.SetActive(false);
            StartCoroutine(start_speed_countdown());
        }

        if (col.gameObject.tag == "Jump_Powerup"){
            _jump_multiplier *= 1.5f;
            col.gameObject.SetActive(false);
            StartCoroutine(start_jump_countdown());
        }

        if (col.gameObject.tag == "Coin" && is_runner)
        {
            col.gameObject.transform.parent.gameObject.SetActive(false);
            if (is_p1)
                GameManager.p1_coin_count++;
            else
                GameManager.p2_coin_count++;
        }

        if (col.gameObject.tag == "Player_hit" && !is_runner)
        {
            is_runner = true;
            col.gameObject.transform.parent.GetComponent<PlayerControl>().is_runner = false;
            col.gameObject.transform.parent.GetComponent<PlayerControl>().soft_reset();
            soft_reset();
            Debug.Log("Player 2 hit");
        }
    }

    IEnumerator start_speed_countdown(){
        int counter = 2;
        while (counter > 0){
            yield return new WaitForSeconds(1);
            counter--;
        }
        _speed_multiplier = 1f;
    }

    IEnumerator start_jump_countdown(){
        int counter = 2;
        while (counter > 0){
            yield return new WaitForSeconds(1);
            counter--;
        }
        _jump_multiplier = 1f;
    }

    public void soft_reset()
    {
        GameManager.is_playing = false;
        transform.position = _init_pos;
        if (is_runner)
            _box_col.SetActive(true);
        else
            _box_col.SetActive(false);
    }

    void    OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Slow_Trap")
            _speed_multiplier = 1f;
    }
}