using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpingController : MonoBehaviour
{
    private Rigidbody _rb;
    private float _maxInitialForce = 50;
    private float _maxJumpDuration = 1.0f;


    private float _currentForce;
    private float _jumpTimeElapsed;
    private bool _jumpReset = false;
    private bool _jumping = false;

    private void Start()
    {
        ResetJump();
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.Space) && !_jumpReset)
        {
            if (_jumpTimeElapsed < _maxJumpDuration)
            {
                _jumping = true;
                _rb.AddForce(Vector3.up * _currentForce);
                _jumpTimeElapsed += Time.fixedDeltaTime;
                RecalculateForce();
            }
        }
        else if(_jumping)
        {
            _jumpReset = true;
            _rb.AddForce(Vector3.down * _maxInitialForce / 2);
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        ResetJump();
    }


    private void ResetJump()
    {
        _currentForce = _maxInitialForce;
        _jumpTimeElapsed = 0.0f;
        _jumping = false;
        _jumpReset = false;
    }

    private void RecalculateForce()
    {
        _currentForce = Mathf.Lerp(0.0f, _maxInitialForce, (_maxJumpDuration - _jumpTimeElapsed) / _maxJumpDuration);
    }
}
