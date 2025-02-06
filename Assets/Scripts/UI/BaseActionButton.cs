using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public abstract class BaseActionButton : MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{

    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _unselectedColor;
    [SerializeField] private Image _background;

    private bool _isClicked = false;

    protected virtual void Start()
    {
        _background.color = _unselectedColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InvokeClickEvent();
        _isClicked = !_isClicked;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _background.color = _selectedColor;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isClicked)
            return;
        _background.color = _unselectedColor;

    }

    public abstract void InvokeClickEvent();
    public void ResetState()
    {
        _background.color = _unselectedColor;
        _isClicked = false;
    }
}
