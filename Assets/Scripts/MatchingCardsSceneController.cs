using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MatchingCardsSceneController : MonoBehaviour
{
    VerseRecord[] verses; int current; int selectedReference=-1; int[] order; Text status;
    void Awake(){if(FindObjectOfType<EventSystem>()==null){var e=new GameObject("EventSystem");e.AddComponent<EventSystem>();e.AddComponent<StandaloneInputModule>();}verses=VerseDatabase.Load();status=GameObject.Find("MatchStatus")?.GetComponent<Text>();for(int i=0;i<4;i++){int n=i;Bind("Reference"+n,()=>SelectReference(n));Bind("Verse"+n,()=>SelectVerse(n));}Bind("NextButton",Next);Bind("BackButton",()=>SceneManager.LoadScene("MaimMenu"));Show();}
    void Bind(string n,UnityEngine.Events.UnityAction a){var b=GameObject.Find(n)?.GetComponent<Button>();if(b!=null){b.onClick.RemoveAllListeners();b.onClick.AddListener(a);}}
    void Show(){if(verses.Length<4)return;selectedReference=-1;if(status!=null)status.text="Elige una referencia y luego su versículo";order=new[]{0,1,2,3};for(int i=3;i>0;i--){int j=Random.Range(0,i+1);int t=order[i];order[i]=order[j];order[j]=t;}for(int i=0;i<4;i++){SetLabel("Reference"+i,verses[current+i].reference);SetLabel("Verse"+i,verses[current+order[i]].text);}}
    void SetLabel(string n,string v){var t=GameObject.Find(n)?.GetComponentInChildren<Text>();if(t!=null)t.text=v;}
    void SelectReference(int n){selectedReference=n;if(status!=null)status.text="Ahora elige el versículo correspondiente";}
    void SelectVerse(int n){if(selectedReference<0){if(status!=null)status.text="Primero elige una referencia";return;}if(status!=null)status.text=order[n]==selectedReference?"¡Coincidencia correcta!":"No coincide; inténtalo de nuevo.";}
    void Next(){current=(current+4)%Mathf.Max(4,verses.Length);Show();}
}

