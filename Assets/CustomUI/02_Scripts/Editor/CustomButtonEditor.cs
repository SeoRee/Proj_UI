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
    SerializedProperty child;

    void OnEnable()
    {
        //Debug.Log("Call onenable");
        //if(customBtn == null)
        //{
        //    customBtn = target as CustomButton;
        //}
         
        if (cb == null)
        {
            cb = target as CustomButton;

            sprite = serializedObject.FindProperty("sprite");
            alphaHit = serializedObject.FindProperty("buttonAlphaHit");
            animationType = serializedObject.FindProperty("buttonType");
            scale = serializedObject.FindProperty("targetScale");
            clickEvents = serializedObject.FindProperty("clickEvents");
            child = serializedObject.FindProperty("childBtnImage");
            child.objectReferenceValue = Selection.activeGameObject.transform.GetChild(0).GetComponent<Image>();
        }
    }

    public override void OnInspectorGUI()
    {
        //if (customBtn == null) return;

        serializedObject.Update();
        EditorGUILayout.PropertyField(sprite);
        EditorGUILayout.PropertyField(alphaHit);
        EditorGUILayout.PropertyField(animationType);
        EditorGUILayout.PropertyField(scale);
        EditorGUILayout.PropertyField(clickEvents);
        serializedObject.ApplyModifiedProperties();

        //Debug.Log(sprite.objectReferenceValue == null);
        if (sprite.objectReferenceValue != null)
        {
            EditorGUILayout.Space();
            if (GUILayout.Button("Apply"))
            {
                Apply();
            }
        }
    }

    void Apply()
    {
        //var images = cb.GetComponentsInChildren<Image>();
        //Debug.Log(images.Length);
        //for (int i = 0; i < images.Length; i++)
        //{
        //    images[i].sprite = sprite.objectReferenceValue as Sprite;

        //    if (images[i].sprite != null)
        //    {
        //        images[i].SetNativeSize();
        //        images[i].rectTransform.sizeDelta *= scale.floatValue;

        //        if (images[i].raycastTarget && cb.buttonAlphaHit == AlphaHit.Hit)
        //            images[i].alphaHitTestMinimumThreshold = 0.1f;
        //    }
        //}

        Image image = child.objectReferenceValue as Image;
        image.sprite = sprite.objectReferenceValue as Sprite;
        image.SetNativeSize();
        image.rectTransform.sizeDelta *= scale.floatValue;
        SerializedObject imageObject = new SerializedObject(image);

        var pop = imageObject.GetIterator();

        while (pop.NextVisible(true))
        {
            Debug.Log(pop.propertyPath);
        }

        //Get latest serializedObject and apply
        imageObject.Update();
        imageObject.ApplyModifiedProperties();

        //var images = Selection.activeGameObject.GetComponentsInChildren<Image>();
        //Debug.Log(images.Length);
        //for (int i = 0; i < images.Length; i++)
        //{
        //    Image image = images[i];
        //    SerializedProperty ImageSprite = serializedObject.FindProperty("images[i].sprite");
        //    ImageSprite.objectReferenceValue = sprite.objectReferenceValue;

        //    if (images[i].sprite != null)
        //    {
        //        images[i].SetNativeSize();
        //        images[i].rectTransform.sizeDelta *= scale.floatValue;

        //        if (images[i].raycastTarget && cb.buttonAlphaHit == AlphaHit.Hit)
        //            images[i].alphaHitTestMinimumThreshold = 0.1f;
        //    }
        //}
    }
}
