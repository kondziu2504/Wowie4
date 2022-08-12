using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class FloatingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public float animTime = 0.5f;
    public float scaling = 1.1f;

    private Button button;

    private bool interactableLastFrame = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!button.interactable)
            return;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(Vector3.one, animTime * 0.1f)
            .SetEase(Ease.InOutCubic);
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable)
            return;

        transform.DOScale(Vector3.one * scaling, animTime).SetEase(Ease.InOutCubic);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(Vector3.one * scaling, animTime)
               .SetEase(Ease.InOutCubic);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable)
            return;

        transform.DOScale(Vector3.one, animTime).SetEase(Ease.InOutCubic);
    }

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactableLastFrame && !button.interactable)
        {
            transform.DOKill();
            transform.localScale = Vector3.one;
        }
    }
}
