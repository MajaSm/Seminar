using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool _isRight;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isRight)
            UIManager.Instance.OnHorizontalInput(1);
        else
            UIManager.Instance.OnHorizontalInput(-1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.OnHorizontalInput(0);
    }

}
