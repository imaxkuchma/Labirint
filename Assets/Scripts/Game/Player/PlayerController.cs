using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private IInputSystem _joystick;
    private CharacterController _controller;
    private Transform _cameraTransform;

    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float _stickMagnitude = 0;
    private bool _canMove;
    private IInputSystem _inputSystem;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        //var gameScreen = UIManager.Instance.GetScreen<IGameScreenView>(ScreenType.GameScreen);
        //SetInput(gameScreen.Joystick);
    }


    public void SetInput(IInputSystem inputSystem)
    {
        _inputSystem = inputSystem;
        _inputSystem.OnStickMove += OnStickMove;
        _inputSystem.OnStickDirection += OnStickDirection;     
    }

    private void OnDisable()
    {
        _inputSystem.OnStickMove -= OnStickMove;
        _inputSystem.OnStickDirection -= OnStickDirection;
    }

    private void OnStickMove(bool stickMove)
    {
        _canMove = stickMove;
    }

    private void OnStickDirection(Vector2 direction)
    {
        var forward = _cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        var _right = new Vector3(forward.z, 0.0f, -forward.x);
        _moveDirection = forward * direction.y + _right * direction.x;
        _stickMagnitude = direction.magnitude;
        _velocity = _moveDirection * 20;
    }

    private void Update()
    {
        if (_canMove)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_moveDirection), 6 * Time.deltaTime);
            _controller?.Move(_velocity * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Events.RaiseOnPlayerDamaged();
        }
    }
}
