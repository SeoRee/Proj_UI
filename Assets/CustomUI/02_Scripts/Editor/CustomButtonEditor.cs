using Animation.BTN;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(CustomButton))]
[CanEditMultipleObjects]
public class CustomButtonEditor : Editor
{
    private CustomButton cb = null;
    SerializedProperty sprite;
    SerializedProperty alphaHit;
    SerializedProperty animationType;
    SerializedProperty scale;
    SerializedProperty clickEvents;
    SerializedProperty childImage;
    SerializedProperty btnRect;

    void OnEnable()
    {

        if (cb == null)
        {
            cb = target as CustomButton;

            sprite = serializedObject.FindProperty("sprite");
            alphaHit = serializedObject.FindProperty("buttonAlphaHit");
            animationType = serializedObject.FindProperty("buttonType");
            scale = serializedObject.FindProperty("targetScale");
            clickEvents = serializedObject.FindProperty("clickEvents");
            childImage = serializedObject.FindProperty("childBtnImage");
            btnRect = serializedObject.FindProperty("btnRect");
            childImage.objectReferenceValue = Selection.activeGameObject.transform.GetChild(0).GetComponent<Image>();
            btnRect.objectReferenceValue = Selection.activeGameObject.GetComponent<RectTransform>();
        }
    }

    public override void OnInspectorGUI()
    {
        //if (customBtn == null) return;

        serializedObject.Update();
        EditorGUILayout.PropertyField(sprite);
        EditorGUILayout.PropertyField(alphaHit);
        EditorGUILayout.PropertyField(animationType);
        EditorGUILayout.PropertyField(clickEvents);

        //Debug.Log(sprite.objectReferenceValue == null);
        if (sprite.objectReferenceValue != null)
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(scale);

            if (GUILayout.Button("Apply"))
            {
                Apply();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    void Apply()
    {
        //Image sprite set to child image component
        {
            Image image = childImage.objectReferenceValue as Image;
            image.sprite = cb.sprite;
            image.SetNativeSize();
            image.rectTransform.sizeDelta *= scale.floatValue;
            SerializedObject so = new SerializedObject(image);

            //Get latest serializedObject and apply
            so.Update();
            so.ApplyModifiedProperties();
        }

        //Set the cover(button) size to image size
        {
            RectTransform rect = btnRect.objectReferenceValue as RectTransform;
            rect.sizeDelta = cb.childBtnImage.rectTransform.sizeDelta;
            SerializedObject so = new SerializedObject(rect);

            //Get latest serializedObject and apply
            so.Update();
            so.ApplyModifiedProperties();

            if (cb.buttonAlphaHit == AlphaHit.Hit)
            {
                //SerializedProperty temp = new SerializedProperty()
                //Image image = imageObj.obj as Image;
                //image.sprite = cb.sprite;
                //image.alphaHitTestMinimumThreshold = 0.1f;
                //SerializedObject so2 = new SerializedObject(image);

                ////Get latest serializedObject and apply
                //so2.Update();
                //so2.ApplyModifiedProperties();
            }
        }
    }
}
