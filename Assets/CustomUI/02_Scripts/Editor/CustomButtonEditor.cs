using Animation.BTN;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(CustomButton))]
[CanEditMultipleObjects]
public class CustomButtonEditor : Editor
{
    CustomButton cb;
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
        //Cach properties from each gameObjects
        if (childImage.objectReferenceValue == null)
        {
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                //serializedOBject.targetObject - CustomButton
                //Selection.gameOBject - GameObject
                if (serializedObject.targetObject.name == Selection.gameObjects[i].name)
                    childImage.objectReferenceValue = Selection.gameObjects[i].transform.GetChild(0).GetComponent<Image>();
            }
        }

        if(btnRect.objectReferenceValue == null)
        {
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                if (serializedObject.targetObject.name == Selection.gameObjects[i].name)
                    btnRect.objectReferenceValue = Selection.gameObjects[i].GetComponent<RectTransform>();
            }
        }

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
            //Overriding or reverting the prefab when there is no sprite
            //in prefab will cause error.
            //Cause : childImage.objectRef is null
            if (childImage.objectReferenceValue != null) Apply();
        }

    }

    void Apply()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            CustomButton customBtn = Selection.gameObjects[i].GetComponent<CustomButton>();
            SerializedObject customBtnSO = new SerializedObject(customBtn);
            SerializedProperty imageProperty = customBtnSO.FindProperty("childBtnImage");
            SerializedProperty spriteProperty = customBtnSO.FindProperty("sprite");
            SerializedProperty scaleProperty = customBtnSO.FindProperty("targetScale");
            SerializedProperty rectProperty = customBtnSO.FindProperty("btnRect");

            Image image = null;
            SerializedObject so = null;

            //Set the rendering(child) image, rectTransform
            {
                image = imageProperty.objectReferenceValue as Image;

                so = new SerializedObject(image);
                //Get latest serializedObject
                so.Update();
                so.FindProperty("m_Sprite").objectReferenceValue = spriteProperty.objectReferenceValue;
                //Apply
                so.ApplyModifiedProperties();

                //Resize the rect
                if (spriteProperty.objectReferenceValue != null)
                {
                    image.SetNativeSize();
                    image.rectTransform.sizeDelta *= scaleProperty.floatValue;
                }
            }

            //Set the cover(parent) image, rectTransform
            {
                //Resize the rect
                RectTransform rect = rectProperty.objectReferenceValue as RectTransform;
                rect.sizeDelta = image.rectTransform.sizeDelta;

                //Set the cover image
                //If use alphahit, apply sprite to image component
                if (customBtn.buttonAlphaHit == AlphaHit.Hit)
                {
                    image = rect.GetComponent<Image>();
                    so = new SerializedObject(image);
                    //Get the property in cover image component
                    SerializedProperty property = so.FindProperty("m_Sprite");
                    so.Update();

                    //Check sprite option for alphaHitThresHold
                    var spriteOption = spriteProperty.objectReferenceValue as Sprite;
                    if (spriteOption != null && spriteOption.texture.isReadable == false)
                    {
                        Debug.LogWarning($"{customBtn.name} : Sprite texture is not readable. Try enable 'Read/Write enabled' to use alphaHitThresHold.");
                        property.objectReferenceValue = null;
                    }
                    else
                    {
                        property.objectReferenceValue = spriteProperty.objectReferenceValue;
                    }

                    so.ApplyModifiedProperties();
                }
            }
        }
    }

    /*
     * ************* Get Properties
     * var properties = SerializedObject.GetIterator();
     * while (properties.NextVisible(true)) Debug.Log(properties.propertyPath);
     * 
     * ************* Chaching check
     * EditorGUILayout.PropertyField(childImage);
     * EditorGUILayout.PropertyField(btnRect);
     */
}
