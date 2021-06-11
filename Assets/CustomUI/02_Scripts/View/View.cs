using UnityEngine;

namespace Mobile.View
{
    public enum Orientation { Horizontal, Vertical}

    public static class View
    {
        //Select orientation : Vertical or Horizontal
        public static float GetOrthographicSize(RectTransform _canvasSize)
        {
            float currentRatio = (float)Screen.width / Screen.height;
            float correctionRatio = _canvasSize.sizeDelta.x / _canvasSize.sizeDelta.y;
            float pixelPerUnitSize = _canvasSize.sizeDelta.y / 2 / 100;

            float correctionSize = correctionRatio / currentRatio;

            return pixelPerUnitSize * correctionSize;
        }
    }
}
