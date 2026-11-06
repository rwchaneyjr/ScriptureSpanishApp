#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[InitializeOnLoad]
public static class MaimMenuSceneBuilder
{
    static MaimMenuSceneBuilder()
    {
        EditorApplication.delayCall += BuildIfNeeded;
    }

    static void BuildIfNeeded()
    {
        if (EditorApplication.isPlaying || SceneManager.GetActiveScene().name != "MaimMenu") return;
        var existingCanvas = GameObject.Find("MaimMenuCanvas");
        if (existingCanvas != null)
        {
            if (GameObject.Find("NewTestamentButton") == null)
                CreateButton("NewTestamentButton", "NUEVO TESTAMENTO", existingCanvas.transform, new Vector2(-180, -240));
            if (GameObject.Find("OldTestamentButton") == null)
                CreateButton("OldTestamentButton", "ANTIGUO TESTAMENTO", existingCanvas.transform, new Vector2(180, -240));
            if (GameObject.Find("MainExitButton") == null)
                CreateButton("MainExitButton", "SALIR", existingCanvas.transform, new Vector2(0, -400));
            if (GameObject.Find("RandomVersesButton") == null)
                CreateButton("RandomVersesButton", "VERSÍCULOS ALEATORIOS", existingCanvas.transform, new Vector2(-180, -520));
            if (GameObject.Find("StandardVersesButton") == null)
                CreateButton("StandardVersesButton", "VERSÍCULOS SECUENCIALES", existingCanvas.transform, new Vector2(180, -520));
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            return;
        }

        var canvasObject = new GameObject("MaimMenuCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        var canvas = canvasObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasObject.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);

        var background = CreatePanel("Background", canvasObject.transform, new Color(.055f, .09f, .16f), Vector2.zero, Vector2.zero);
        Stretch(background);
        CreateText("Title", "Memoria\nBíblica", 64, new Color(.93f, .70f, .28f), canvasObject.transform, new Vector2(0, 720), new Vector2(900, 180));
        CreateText("Subtitle", "Elige un modo de práctica", 26, new Color(.97f, .94f, .86f), canvasObject.transform, new Vector2(0, 570), new Vector2(900, 60));
        CreateButton("FlashcardsButton", "TARJETAS", canvasObject.transform, new Vector2(0, 260));
        CreateButton("FillInBlanksButton", "COMPLETA EL VERSÍCULO", canvasObject.transform, new Vector2(0, 100));
        CreateButton("MatchingCardsButton", "EMPAREJA LAS TARJETAS", canvasObject.transform, new Vector2(0, -60));
        CreateButton("NewTestamentButton", "NUEVO TESTAMENTO", canvasObject.transform, new Vector2(-180, -240));
        CreateButton("OldTestamentButton", "ANTIGUO TESTAMENTO", canvasObject.transform, new Vector2(180, -240));
        CreateButton("MainExitButton", "SALIR", canvasObject.transform, new Vector2(0, -400));
        CreateButton("RandomVersesButton", "VERSÍCULOS ALEATORIOS", canvasObject.transform, new Vector2(-180, -520));
        CreateButton("StandardVersesButton", "VERSÍCULOS SECUENCIALES", canvasObject.transform, new Vector2(180, -520));
        CreateText("Footer", "Memoriza las Escrituras • Crece en la fe", 20, new Color(.75f, .78f, .84f), canvasObject.transform, new Vector2(0, -690), new Vector2(800, 50));
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    }

    static RectTransform CreatePanel(string name, Transform parent, Color color, Vector2 position, Vector2 size)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        var rect = go.GetComponent<RectTransform>(); rect.anchoredPosition = position; rect.sizeDelta = size; go.GetComponent<Image>().color = color; return rect;
    }
    static void Stretch(RectTransform rect){rect.anchorMin=Vector2.zero;rect.anchorMax=Vector2.one;rect.offsetMin=Vector2.zero;rect.offsetMax=Vector2.zero;}
    static void CreateText(string name,string value,int size,Color color,Transform parent,Vector2 position,Vector2 dimensions)
    { var go=new GameObject(name,typeof(RectTransform),typeof(Text));go.transform.SetParent(parent,false);var rect=go.GetComponent<RectTransform>();rect.anchoredPosition=position;rect.sizeDelta=dimensions;var text=go.GetComponent<Text>();text.text=value;text.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");text.fontSize=size;text.color=color;text.alignment=TextAnchor.MiddleCenter;text.fontStyle=FontStyle.Bold;text.horizontalOverflow=HorizontalWrapMode.Wrap; }
    static void CreateButton(string name,string label,Transform parent,Vector2 position)
    { var rect=CreatePanel(name,parent,new Color(.13f,.20f,.30f),position,new Vector2(700,110));rect.gameObject.AddComponent<Button>();CreateText("Label",label,24,new Color(.97f,.94f,.86f),rect,Vector2.zero,new Vector2(680,100)); }
}
#endif

