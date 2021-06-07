using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PrefabCtreateEditor : Editor
{
    [MenuItem("GameObject/Custom UI/Button", false, 0)]
    static void CreateButton()
    {
        var prefabPath = "Assets/CustomUI/03_Prefabs/Button.prefab";
        ButtonCreator(prefabPath);
    }

    [MenuItem("GameObject/Custom UI/Button with text", false, 1)]
    static void CreateButtonWithText()
    {
        var prefabPath = "Assets/CustomUI/03_Prefabs/ButtonWithText.prefab";
        ButtonCreator(prefabPath);
    }

    private static void ButtonCreator(string _prefabPath)
    {
        //Get gameobject selection info
        GameObject gameObjectSelection = Selection.activeGameObject;

        //Get prefab info
        Selection.activeObject = AssetDatabase.LoadAssetAtPath(_prefabPath, typeof(GameObject));

        if (Selection.activeObject == null)
        {
            Debug.LogWarning("Button prefab is missing.");
            return;
        }

        Transform canvasTr = null;

        //Canvas doesnt exist or not selected
        if (gameObjectSelection == null)
        {
            var canvasList = FindObjectsOfType<Canvas>();
            canvasTr = canvasList.Length == 0 ? null : canvasList[0].transform;

            if (canvasTr == null) canvasTr = CreateCanvas();
        }
        else
        {
            //Check the parent of canvas type
            var canvasCheck = gameObjectSelection.GetComponentInParent<Canvas>();

            if (canvasCheck == null)
            {
                canvasTr = CreateCanvas();
            }
            else
            {
                canvasTr = gameObjectSelection.transform;
            }
        }

        //Create prefab in canvas
        Object obj = PrefabUtility.InstantiatePrefab(Selection.activeObject, canvasTr);
        obj.name = "Button prefab";

        //Change current selection info
        Selection.activeObject = obj;
    }

    private static Transform CreateCanvas()
    {
        var canvasObj = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));

        //Specify canvas render mode
        canvasObj.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        return canvasObj.transform;
    }
}
