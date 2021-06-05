using System.Collections;
using UnityEngine;          //Location : Data/Managed/UnityEngine.CoreModule.dll
using UnityEngine.UI;

namespace Animation.BTN
{
    public enum ButtonAnimationType { None, All, Color, Size }
    public enum ButtonShape { Square, Circle }
    public class ButtonAnim
    {
        public static void Play(Image _btnImage, bool _expand)
        {
            _btnImage.color = _expand ? Color.white : Color.gray;
        }

        public static IEnumerator Play(RectTransform _btnRect, bool _expand)
        {
            float lerpTime = 0f;
            Vector2 startScale = _btnRect.localScale;

            while (true)
            {
                lerpTime += Time.unscaledDeltaTime * 15;
                lerpTime = Mathf.Clamp01(lerpTime);

                if (!_expand)
                {
                    _btnRect.localScale = Vector2.Lerp(startScale, Vector2.one * 0.9f, lerpTime);
                }
                else
                {
                    _btnRect.localScale = Vector2.Lerp(startScale, Vector2.one, lerpTime);
                }

                if (lerpTime.Equals(1)) break;

                yield return null;
            }
        }

        public static IEnumerator Play(RectTransform _btnRect, Image _windowImage, bool _expand)
        {
            float lerpTime = 0f;
            Vector2 startScale = _btnRect.localScale;
            Color startColor = _windowImage.color;

            while (true)
            {
                lerpTime += Time.unscaledDeltaTime * 15;
                lerpTime = Mathf.Clamp01(lerpTime);

                if (!_expand)
                {
                    _btnRect.localScale = Vector2.Lerp(startScale, Vector2.one * 0.9f, lerpTime);
                    _windowImage.color = Color.Lerp(startColor, Color.gray, lerpTime);
                }
                else
                {
                    _btnRect.localScale = Vector2.Lerp(startScale, Vector2.one, lerpTime);
                    _windowImage.color = Color.Lerp(startColor, Color.white, lerpTime);
                }

                if (lerpTime.Equals(1)) break;

                yield return null;
            }
        }
    }
}
