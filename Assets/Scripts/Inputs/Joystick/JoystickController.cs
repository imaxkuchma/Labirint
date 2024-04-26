using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class JoystickController : MonoBehaviour, IInputSystem, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private GameObject _joystick;
    [SerializeField] private GameObject _stick;
    [SerializeField] private float _dragRadius;

    private Vector2 _startPos;

    public event Action<bool> OnStickMove;
    public event Action<Vector2> OnStickDirection;

    private void Awake()
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width / 2, 0);
    }

    private void OnEnable()
    {
        _joystick.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var position = eventData.position;

        var distance = Vector2.Distance(_startPos, position);
        distance = Mathf.Clamp(distance, 0, _dragRadius);
        var dir = (eventData.position - _startPos).normalized;
        _stick.transform.localPosition = dir * distance;

        var precent = distance / _dragRadius;
        OnStickDirection?.Invoke(dir * precent);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _startPos = eventData.position;
        _joystick.SetActive(true);
        _stick.transform.localPosition = Vector3.zero;
        _joystick.transform.position = eventData.position;

        OnStickMove?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystick.SetActive(false);
        OnStickMove?.Invoke(false);
    }

    private void OnDisable()
    {
        OnStickMove?.Invoke(false);
    }
}
