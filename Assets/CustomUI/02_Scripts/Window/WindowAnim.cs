using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//new Color(1f, 1f, 1f, 0.95f);
//new Color(1f, 1f, 1f, 0);

namespace Animation.Window
{
    public static class WindowAnim
    {

        public static IEnumerator WindowActiveCoroutine(GraphicRaycaster _ray, Image _backGround, RectTransform _window)
        {
            _ray.enabled = false;

            bool _active = !_window.gameObject.activeSelf;

            Color targetColor = new Color(0f, 0f, 0f, 0.9f);
            Color clearColor = Color.clear;
            float lerpTime = 0f;

            _window.gameObject.SetActive(_active);

            if (_active)
            {
                _backGround.gameObject.SetActive(_active);

                while (true)
                {
                    lerpTime += Time.unscaledDeltaTime * 8;
                    lerpTime = Mathf.Clamp01(lerpTime);

                    _backGround.color = Color.Lerp(clearColor, targetColor, lerpTime);
                    _window.localScale = Vector2.Lerp(Vector2.zero, Vector2.one, lerpTime);

                    if (lerpTime.Equals(1))
                    {
                        break;
                    }

                    yield return null;
                }
            }
            else
            {
                while (true)
                {
                    lerpTime += Time.unscaledDeltaTime * 8;
                    lerpTime = Mathf.Clamp01(lerpTime);

                    _backGround.color = Color.Lerp(targetColor, clearColor, lerpTime);

                    if (lerpTime.Equals(1))
                    {
                        _backGround.gameObject.SetActive(_active);
                        break;
                    }

                    yield return null;
                }
            }

            _ray.enabled = true;
        }

        public static IEnumerator MultiWindowActiveCoroutine(GraphicRaycaster _ray, Image[] _backGrounds, params RectTransform[] _windows)
        {
            _ray.enabled = false;

            bool _active = !_windows[0].gameObject.activeSelf;

            Color targetColor = new Color(0f, 0f, 0f, 0.9f);
            Color clearColor = Color.clear;
            float lerpTime = 0f;

            int arrayIndex = 0;
            _windows[arrayIndex].gameObject.SetActive(_active);

            if (_active)
            {
                for (int i = 0; i < _windows.Length; i++)
                    _backGrounds[i].gameObject.SetActive(_active);

                while (true)
                {
                    lerpTime += Time.unscaledDeltaTime * 8;
                    lerpTime = Mathf.Clamp01(lerpTime);

                    _backGrounds[arrayIndex].color = Color.Lerp(clearColor, targetColor, lerpTime);
                    _windows[arrayIndex].localScale = Vector2.Lerp(Vector2.zero, Vector2.one, lerpTime);


                    if (lerpTime.Equals(1))
                    {
                        lerpTime = 0;
                        arrayIndex++;
                        if (arrayIndex >= _windows.Length)
                        {
                            break;
                        }
                        else
                        {
                            _windows[arrayIndex].gameObject.SetActive(_active);
                        }
                    }

                    yield return null;
                }
            }
            //else
            //{
            //    //while (true)
            //    //{
            //    //    lerpTime += Time.unscaledDeltaTime * 8;
            //    //    lerpTime = Mathf.Clamp01(lerpTime);

            //    //    for (int i = 0; i < _windows.Length; i++)
            //    //        _backGrounds[i].color = Color.Lerp(targetColor, clearColor, lerpTime);

            //    //    if (lerpTime.Equals(1))
            //    //    {
            //    //        for (int i = 0; i < _windows.Length; i++)
            //    //            _backGrounds[i].gameObject.SetActive(_active);
            //    //        break;
            //    //    }

            //    //    yield return null;
            //    //}
            //}

            _ray.enabled = true;
        }

        public static IEnumerator ChangeWindowCoroutine(GraphicRaycaster _ray, params RectTransform[] _windows)
        {
            if (_windows.Length.Equals(1))
            {
                float lerpTime = 0;
                while (true)
                {
                    lerpTime += Time.unscaledDeltaTime * 8;
                    lerpTime = Mathf.Clamp01(lerpTime);

                    _windows[0].localScale = Vector2.Lerp(Vector2.zero, Vector2.one, lerpTime);

                    if (lerpTime.Equals(1))
                    {
                        break;
                    }

                    yield return null;
                }
            }
            else
            {
                _windows[0].gameObject.SetActive(false);
                _windows[1].gameObject.SetActive(true);
                float lerpTime = 0;
                while (true)
                {
                    lerpTime += Time.unscaledDeltaTime * 8;
                    lerpTime = Mathf.Clamp01(lerpTime);

                    _windows[1].localScale = Vector2.Lerp(Vector2.zero, Vector2.one, lerpTime);

                    if (lerpTime.Equals(1))
                    {
                        break;
                    }

                    yield return null;
                }
            }
        }

        public static IEnumerator WindowActiveCoroutine(Image _backGround, RectTransform _window)
        {
            bool _active = !_window.gameObject.activeSelf;

            Color targetColor = new Color(0f, 0f, 0f, 0.9f);
            Color clearColor = Color.clear;
            float lerpTime = 0f;

            _window.gameObject.SetActive(_active);

            if (_active)
            {
                _backGround.gameObject.SetActive(_active);

                while (true)
                {
                    lerpTime += Time.unscaledDeltaTime * 8;
                    lerpTime = Mathf.Clamp01(lerpTime);

                    _backGround.color = Color.Lerp(clearColor, targetColor, lerpTime);
                    _window.localScale = Vector2.Lerp(Vector2.zero, Vector2.one, lerpTime);

                    if (lerpTime.Equals(1))
                    {
                        break;
                    }

                    yield return null;
                }
            }
            else
            {
                while (true)
                {
                    lerpTime += Time.unscaledDeltaTime * 8;
                    lerpTime = Mathf.Clamp01(lerpTime);

                    _backGround.color = Color.Lerp(targetColor, clearColor, lerpTime);

                    if (lerpTime.Equals(1))
                    {
                        _backGround.gameObject.SetActive(_active);
                        break;
                    }

                    yield return null;
                }
            }
        }
    }
}
