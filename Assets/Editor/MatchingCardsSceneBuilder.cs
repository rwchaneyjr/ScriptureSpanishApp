#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[InitializeOnLoad] public static class MatchingCardsSceneBuilder
{
    static MatchingCardsSceneBuilder(){EditorSceneManager.sceneOpened+=Opened;EditorApplication.delayCall+=Build;}
    static void Opened(Scene s,OpenSceneMode m){if(s.name=="MatchingCards")EditorApplication.delayCall+=Build;}
    [MenuItem("ScriptureApp/Build Empareja las tarjetas Scene")]
    static void BuildMenu(){Build();}
    static void Build(){if(EditorApplication.isPlaying||SceneManager.GetActiveScene().name!="MatchingCards"||GameObject.Find("MatchingCardsCanvas")!=null)return;var root=new GameObject("MatchingCardsCanvas",typeof(Canvas),typeof(CanvasScaler),typeof(GraphicRaycaster));root.GetComponent<Canvas>().renderMode=RenderMode.ScreenSpaceOverlay;var sc=root.GetComponent<CanvasScaler>();sc.uiScaleMode=CanvasScaler.ScaleMode.ScaleWithScreenSize;sc.referenceResolution=new Vector2(1200,2160);var bg=Panel("Background",root.transform,new Color(.055f,.09f,.16f),Vector2.zero,new Vector2(1080,1920));bg.anchorMin=Vector2.zero;bg.anchorMax=Vector2.one;bg.sizeDelta=Vector2.zero;Text("Title","Empareja las tarjetas",50,new Color(.93f,.70f,.28f),root.transform,new Vector2(0,720),new Vector2(900,100));Text("Instruction","Relaciona cada referencia con su versículo",25,new Color(.97f,.94f,.86f),root.transform,new Vector2(0,610),new Vector2(900,60));for(int i=0;i<4;i++){float y=260-i*150;Button("Reference"+i,"Reference",root.transform,new Vector2(-220,y),new Vector2(390,115));Button("Verse"+i,"Verse",root.transform,new Vector2(220,y),new Vector2(390,115));}Text("MatchStatus","Elige una referencia y luego su versículo",22,new Color(.93f,.70f,.28f),root.transform,new Vector2(0,-430),new Vector2(900,70));Button("NextButton","SIGUIENTE ROUND",root.transform,new Vector2(0,-560),new Vector2(500,80));Button("BackButton","VOLVER AL MENÚ",root.transform,new Vector2(0,-680),new Vector2(420,75));EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());EditorSceneManager.SaveScene(SceneManager.GetActiveScene());}
    static RectTransform Panel(string n,Transform p,Color c,Vector2 pos,Vector2 size){var g=new GameObject(n,typeof(RectTransform),typeof(Image));g.transform.SetParent(p,false);var r=g.GetComponent<RectTransform>();r.anchoredPosition=pos;r.sizeDelta=size;g.GetComponent<Image>().color=c;return r;}
    static void Text(string n,string v,int s,Color c,Transform p,Vector2 pos,Vector2 size){var g=new GameObject(n,typeof(RectTransform),typeof(Text));g.transform.SetParent(p,false);var r=g.GetComponent<RectTransform>();r.anchoredPosition=pos;r.sizeDelta=size;var t=g.GetComponent<Text>();t.text=v;t.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");t.fontSize=s;t.color=c;t.alignment=TextAnchor.MiddleCenter;t.fontStyle=FontStyle.Bold;t.horizontalOverflow=HorizontalWrapMode.Wrap;}
    static void Button(string n,string label,Transform p,Vector2 pos,Vector2 size){var r=Panel(n,p,new Color(.13f,.20f,.30f),pos,size);r.gameObject.AddComponent<Button>();Text("Label",label,20,new Color(.97f,.94f,.86f),r,Vector2.zero,size-new Vector2(20,10));r.GetComponentInChildren<Text>().fontStyle=FontStyle.Bold;}
}
#endif

