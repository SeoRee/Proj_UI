using Animation.BTN;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[ExecuteInEditMode]
public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Sprite sprite;
    public Image childBtnImage = null;
    public RectTransform btnRect = null;
    private IEnumerator buttonAnimCoroutine = null;

    public AlphaHit buttonAlphaHit = AlphaHit.None;
    public ButtonAnimationType buttonType = ButtonAnimationType.None;

    [Range(0f, 1f)]
    public float targetScale = 1f;          //Specify button scale

    [Space(15)]
    public UnityEvent clickEvents;          //Input fuctions

    private void Start()
    {
        if (buttonAlphaHit == AlphaHit.Hit)
        {
            var image = GetComponent<Image>();
            image.alphaHitTestMinimumThreshold = 0.1f;
        }
    }

    private void PlayButtonAnim(bool _expand)
    {
        if (buttonAnimCoroutine != null)
        {
            StopCoroutine(buttonAnimCoroutine);
        }

        switch (buttonType)
        {
            case ButtonAnimationType.All:
                buttonAnimCoroutine = ButtonAnim.Play(childBtnImage.rectTransform, childBtnImage, _expand);
                StartCoroutine(buttonAnimCoroutine);
                break;
            case ButtonAnimationType.Color:
                ButtonAnim.Play(childBtnImage, _expand);
                break;
            case ButtonAnimationType.Size:
                buttonAnimCoroutine = ButtonAnim.Play(childBtnImage.rectTransform, _expand);
                StartCoroutine(buttonAnimCoroutine);
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        if (childBtnImage != null)
        {
            childBtnImage.rectTransform.localScale = Vector2.one;
            childBtnImage.color = buttonType switch
            {
                ButtonAnimationType.Size => childBtnImage.color,
                _ => Color.white
            };
        }
    }

    #region Pointer interface
    public void OnPointerDown(PointerEventData _data)
    {
        PlayButtonAnim(false);
    }

    public void OnPointerUp(PointerEventData _data)
    {
        PlayButtonAnim(true);
    }

    public void OnPointerClick(PointerEventData _data)
    {
        if (clickEvents != null)
        {
            clickEvents.Invoke();
        }
    }
    #endregion
}
