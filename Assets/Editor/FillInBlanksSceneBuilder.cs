#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[InitializeOnLoad]
public static class FillInBlanksSceneBuilder
{
    static FillInBlanksSceneBuilder(){EditorSceneManager.sceneOpened+=OnSceneOpened;EditorApplication.delayCall+=BuildIfNeeded;}
    static void OnSceneOpened(Scene scene,OpenSceneMode mode){if(scene.name=="FillInBlanks")EditorApplication.delayCall+=BuildIfNeeded;}
    static void BuildIfNeeded(){if(EditorApplication.isPlaying||SceneManager.GetActiveScene().name!="FillInBlanks")return;if(GameObject.Find("FillInBlanksCanvas")!=null)return;var root=new GameObject("FillInBlanksCanvas",typeof(Canvas),typeof(CanvasScaler),typeof(GraphicRaycaster));var canvas=root.GetComponent<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceOverlay;var scale=root.GetComponent<CanvasScaler>();scale.uiScaleMode=CanvasScaler.ScaleMode.ScaleWithScreenSize;scale.referenceResolution=new Vector2(1200,2160);var bg=Panel("Background",root.transform,new Color(.055f,.09f,.16f),Vector2.zero,new Vector2(1080,1920));bg.anchorMin=Vector2.zero;bg.anchorMax=Vector2.one;bg.sizeDelta=Vector2.zero;Text("Title","Fill in the Blank",50,new Color(.93f,.70f,.28f),root.transform,new Vector2(0,720),new Vector2(900,100));Text("Instruction","Choose the missing word",25,new Color(.97f,.94f,.86f),root.transform,new Vector2(0,610),new Vector2(900,60));var card=Panel("VerseCard",root.transform,new Color(.96f,.91f,.78f),new Vector2(0,120),new Vector2(820,520));Text("Reference","Hebrews 11:1",38,new Color(.055f,.09f,.16f),card,new Vector2(0,150),new Vector2(700,80));Text("VerseText","Now faith is ____ of things hoped for.",28,new Color(.055f,.09f,.16f),card,Vector2.zero,new Vector2(700,180));Text("Feedback","Choose an answer",22,new Color(.93f,.70f,.28f),root.transform,new Vector2(0,-170),new Vector2(800,60));Button("AnswerA","assurance",root.transform,new Vector2(-210,-290));Button("AnswerB","world",root.transform,new Vector2(210,-290));Button("AnswerC","strength",root.transform,new Vector2(-210,-400));Button("AnswerD","wisdom",root.transform,new Vector2(210,-400));Button("NextButton","NEXT VERSE",root.transform,new Vector2(0,-540),new Vector2(500,80));Button("BackButton","BACK TO MENU",root.transform,new Vector2(0,-660),new Vector2(420,75));EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());EditorSceneManager.SaveScene(SceneManager.GetActiveScene());}
    static RectTransform Panel(string n,Transform p,Color c,Vector2 pos,Vector2 size){var g=new GameObject(n,typeof(RectTransform),typeof(Image));g.transform.SetParent(p,false);var r=g.GetComponent<RectTransform>();r.anchoredPosition=pos;r.sizeDelta=size;g.GetComponent<Image>().color=c;return r;}
    static void Text(string n,string v,int s,Color c,Transform p,Vector2 pos,Vector2 size){var g=new GameObject(n,typeof(RectTransform),typeof(Text));g.transform.SetParent(p,false);var r=g.GetComponent<RectTransform>();r.anchoredPosition=pos;r.sizeDelta=size;var t=g.GetComponent<Text>();t.text=v;t.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");t.fontSize=s;t.color=c;t.alignment=TextAnchor.MiddleCenter;t.horizontalOverflow=HorizontalWrapMode.Wrap;}
    static void Button(string n,string label,Transform p,Vector2 pos){Button(n,label,p,pos,new Vector2(360,85));}
    static void Button(string n,string label,Transform p,Vector2 pos,Vector2 size){var r=Panel(n,p,new Color(.13f,.20f,.30f),pos,size);r.gameObject.AddComponent<Button>();Text("Label",label,23,new Color(.97f,.94f,.86f),r,Vector2.zero,size-new Vector2(20,10));r.GetComponentInChildren<Text>().fontStyle=FontStyle.Bold;}
}
#endif
