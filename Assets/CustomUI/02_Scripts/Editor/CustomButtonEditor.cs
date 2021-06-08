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
        }
    }

    public override void OnInspectorGUI()
    {
        if (cb == null) return;

        serializedObject.Update();
        //Cash properties
        childImage.objectReferenceValue = Selection.activeGameObject.transform.GetChild(0).GetComponent<Image>();
        btnRect.objectReferenceValue = Selection.activeGameObject.GetComponent<RectTransform>();
        //Show properties
        EditorGUILayout.PropertyField(sprite);
        EditorGUILayout.PropertyField(animationType);
        EditorGUILayout.PropertyField(clickEvents);

        serializedObject.ApplyModifiedProperties();

        if (sprite.objectReferenceValue != null)
        {
            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(alphaHit);
            EditorGUILayout.PropertyField(scale);
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Apply")) Apply();
        }
        else
        {
            Apply();
        }

    }

    void Apply()
    {
        //Image sprite set to child image component
        {
            Image image = childImage.objectReferenceValue as Image;

            SerializedObject so = new SerializedObject(image);
            //Get latest serializedObject
            so.Update();
            so.FindProperty("m_Sprite").objectReferenceValue = sprite.objectReferenceValue;
            //Apply
            so.ApplyModifiedProperties();

            //Resize the rect
            if (sprite.objectReferenceValue != null)
            {
                image.SetNativeSize();
                image.rectTransform.sizeDelta *= scale.floatValue;
            }
        }

        //Set the cover(button) size to image size
        {
            RectTransform rect = btnRect.objectReferenceValue as RectTransform;
            rect.sizeDelta = cb.childBtnImage.rectTransform.sizeDelta;

            //If use alphahit, apply sprite to image component
            if (cb.buttonAlphaHit == AlphaHit.Hit)
            {
                Image image = rect.GetComponent<Image>();
                SerializedObject so = new SerializedObject(image);
                SerializedProperty property = so.FindProperty("m_Sprite");
                so.Update();

                //Check sprite option for alphaHitThresHold
                var spriteOption = sprite.objectReferenceValue as Sprite;
                if(spriteOption != null && spriteOption.texture.isReadable == false)
                {
                    Debug.LogWarning("Sprite texture is not readable. Try enable 'Read/Write enabled' to use alphaHitThresHold.");
                    property.objectReferenceValue = null;
                }
                else
                {
                    property.objectReferenceValue = sprite.objectReferenceValue;
                }

                so.ApplyModifiedProperties();
            }
        }
    }

    /*
     * ************* Get Properties
     * var properties = SerializedObject.GetIterator();
     * while (properties.NextVisible(true)) Debug.Log(properties.propertyPath);
     */
}
