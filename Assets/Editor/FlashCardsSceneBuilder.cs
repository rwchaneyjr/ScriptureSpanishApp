#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[InitializeOnLoad]
public static class FlashCardsSceneBuilder
{
    static FlashCardsSceneBuilder() { EditorSceneManager.sceneOpened += OnSceneOpened; EditorApplication.delayCall += BuildIfNeeded; }
    static void OnSceneOpened(Scene scene, OpenSceneMode mode) { if (scene.name == "FlashCards") EditorApplication.delayCall += BuildIfNeeded; }
    static void BuildIfNeeded()
    {
        if (EditorApplication.isPlaying || SceneManager.GetActiveScene().name != "FlashCards") return;
        var existingCanvas = GameObject.Find("FlashCardsCanvas");
        if (existingCanvas != null)
        {
            if (GameObject.Find("QuitButton") == null)
            {
                Button("PreviousButton", "PREVIOUS", existingCanvas.transform, new Vector2(-160, -380), new Vector2(280, 85));
                Button("QuitButton", "QUIT", existingCanvas.transform, new Vector2(0, -600), new Vector2(420, 75));
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            }
            return;
        }
        var canvasObject=new GameObject("FlashCardsCanvas",typeof(Canvas),typeof(CanvasScaler),typeof(GraphicRaycaster));
        var canvas=canvasObject.GetComponent<Canvas>(); canvas.renderMode=RenderMode.ScreenSpaceOverlay;
        var scaler=canvasObject.GetComponent<CanvasScaler>(); scaler.uiScaleMode=CanvasScaler.ScaleMode.ScaleWithScreenSize; scaler.referenceResolution=new Vector2(1200,2160);
        var bg=Panel("Background",canvasObject.transform,new Color(.055f,.09f,.16f),Vector2.zero,Vector2.zero); bg.anchorMin=Vector2.zero; bg.anchorMax=Vector2.one; bg.offsetMin=Vector2.zero; bg.offsetMax=Vector2.zero;
        Text("Title","Flashcards",52,new Color(.93f,.70f,.28f),canvasObject.transform,new Vector2(0,720),new Vector2(900,100));
        Text("Instruction","Think of the verse, then reveal the card",24,new Color(.97f,.94f,.86f),canvasObject.transform,new Vector2(0,610),new Vector2(900,60));
        var card=Panel("VerseCard",canvasObject.transform,new Color(.96f,.91f,.78f),new Vector2(0,80),new Vector2(820,580));
        Text("Reference","John 3:16",42,new Color(.055f,.09f,.16f),card,new Vector2(0,35),new Vector2(700,100));
        Text("Prompt","Tap reveal to see the verse",25,new Color(.22f,.25f,.30f),card,new Vector2(0,-70),new Vector2(700,80));
        Button("RevealButton","TAP CARD / REVEAL",canvasObject.transform,new Vector2(0,-250),new Vector2(600,95));
        Button("PreviousButton","PREVIOUS",canvasObject.transform,new Vector2(-160,-380),new Vector2(280,85));
        Button("NextButton","NEXT",canvasObject.transform,new Vector2(160,-380),new Vector2(280,85));
        Button("BackButton","BACK TO MENU",canvasObject.transform,new Vector2(0,-500),new Vector2(420,75));
        Button("QuitButton","QUIT",canvasObject.transform,new Vector2(0,-600),new Vector2(420,75));
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene()); EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    }
    static RectTransform Panel(string name,Transform parent,Color color,Vector2 pos,Vector2 size){var go=new GameObject(name,typeof(RectTransform),typeof(Image));go.transform.SetParent(parent,false);var r=go.GetComponent<RectTransform>();r.anchoredPosition=pos;r.sizeDelta=size;go.GetComponent<Image>().color=color;return r;}
    static void Text(string name,string value,int size,Color color,Transform parent,Vector2 pos,Vector2 dims){var go=new GameObject(name,typeof(RectTransform),typeof(Text));go.transform.SetParent(parent,false);var r=go.GetComponent<RectTransform>();r.anchoredPosition=pos;r.sizeDelta=dims;var t=go.GetComponent<Text>();t.text=value;t.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");t.fontSize=size;t.color=color;t.alignment=TextAnchor.MiddleCenter;t.horizontalOverflow=HorizontalWrapMode.Wrap;}
    static void Button(string name,string label,Transform parent,Vector2 pos,Vector2 size){var r=Panel(name,parent,new Color(.13f,.20f,.30f),pos,size);r.gameObject.AddComponent<Button>();Text("Label",label,24,new Color(.97f,.94f,.86f),r,Vector2.zero,size-new Vector2(20,10));}
}
#endif
