using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _jumpForce;

    private float _horizontalInput;
    private int _numOfJumps=0;
    private Rigidbody _rb;
    private bool _isPlayerAbleToMove = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        SetIsPlayerAbleToMove(false);
    }
   
    // Update is called once per frame
    void Update()
    {
        if (!_isPlayerAbleToMove)
        {
            _rb.velocity = Vector3.zero;
            return;
        }
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _speed * _horizontalInput);  
        
        GetInputFromKeyboard();
    }

    private void GetInputFromKeyboard()
    {
        _horizontalInput = 0;
        if (Input.GetKey(KeyCode.A))
            _horizontalInput = -1;
        else if (Input.GetKey(KeyCode.D))
            _horizontalInput = 1;

        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _speed * _horizontalInput);
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

    }

    public void Jump()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.08f))
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _jumpForce, _rb.velocity.z);
            _numOfJumps = 1;
        }
        else if (_numOfJumps < 2)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _jumpForce, _rb.velocity.z);
            _numOfJumps++;
        }
    }
    public void SetHorizontalInput(float value)
    {
        _horizontalInput = value;
    }

    public void SetIsPlayerAbleToMove(bool canMove)
    {
        _isPlayerAbleToMove = canMove;

        _rb.isKinematic = !_isPlayerAbleToMove;
    }

    public void SetSpeed( float speed)
    {
        _speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KillPlayer")
        {
            GameManager.Instance.OnPlayerDeath();
            Debug.Log("Player Killed");
        }
        if (other.tag == "Finish")
        {
            GameManager.Instance.OnPlayerFinish();
            Debug.Log("You are finish this level!");
        }
    }
    public void OnResetPlayer()
    {
        transform.position = Vector3.zero;
        SetIsPlayerAbleToMove(false);
    }
}
