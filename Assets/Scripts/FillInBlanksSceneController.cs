using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FillInBlanksSceneController : MonoBehaviour
{
    VerseRecord[] verses; int current; string answer;
    Text reference, verseText, feedback;
    void Awake(){if(FindObjectOfType<EventSystem>()==null){var e=new GameObject("EventSystem");e.AddComponent<EventSystem>();e.AddComponent<StandaloneInputModule>();}verses=VerseDatabase.Load();reference=GameObject.Find("Reference")?.GetComponent<Text>();verseText=GameObject.Find("VerseText")?.GetComponent<Text>();feedback=GameObject.Find("Feedback")?.GetComponent<Text>();CreateQuitButton();Bind("AnswerA",()=>Check(GameObject.Find("AnswerA")));Bind("AnswerB",()=>Check(GameObject.Find("AnswerB")));Bind("AnswerC",()=>Check(GameObject.Find("AnswerC")));Bind("AnswerD",()=>Check(GameObject.Find("AnswerD")));Bind("NextButton",Next);Bind("BackButton",()=>SceneManager.LoadScene("MaimMenu"));Bind("QuitButton",Quit);Show();}
    void Bind(string name,UnityEngine.Events.UnityAction action){var go=GameObject.Find(name);if(go!=null){var b=go.GetComponent<Button>();if(b!=null){b.onClick.RemoveAllListeners();b.onClick.AddListener(action);}}}
    void Show(){if(verses.Length==0)return;var v=verses[current];var words=v.text.Split(' ');int blank=Mathf.Min(2,words.Length-1);answer=words[blank].Trim(new[]{',','.',';',':','!','?'});words[blank]="____";if(reference!=null)reference.text=v.reference;if(verseText!=null)verseText.text=string.Join(" ",words);if(feedback!=null)feedback.text="Choose the missing word";var options=new[]{answer,"world","light","strength"};for(int i=options.Length-1;i>0;i--){int j=Random.Range(0,i+1);string temp=options[i];options[i]=options[j];options[j]=temp;}SetLabel("AnswerA",options[0]);SetLabel("AnswerB",options[1]);SetLabel("AnswerC",options[2]);SetLabel("AnswerD",options[3]);}
    void SetLabel(string name,string value){var go=GameObject.Find(name);if(go!=null){var t=go.GetComponentInChildren<Text>();if(t!=null)t.text=value;}}
    void Check(GameObject button){var t=button==null?null:button.GetComponentInChildren<Text>();if(feedback!=null)feedback.text=t!=null&&t.text==answer?"Correct!":"Try again.";}
    void CreateQuitButton(){if(GameObject.Find("QuitButton")!=null)return;var back=GameObject.Find("BackButton");if(back==null)return;var go=Instantiate(back,back.transform.parent);go.name="QuitButton";go.GetComponent<RectTransform>().anchoredPosition+=new Vector2(0,-100);SetLabel("QuitButton","EXIT APP");}
    void Quit(){Application.Quit();}
    void Next(){current=(current+1)%verses.Length;Show();}
}
