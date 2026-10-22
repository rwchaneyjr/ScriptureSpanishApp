using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlashCardsSceneController : MonoBehaviour
{
    VerseRecord[] verses; int current; bool revealed;
    Text reference, prompt, status;
    void Awake()
    {
        verses = VerseDatabase.Load();
        if (verses.Length == 0) return;
        reference = GameObject.Find("Reference")?.GetComponent<Text>();
        prompt = GameObject.Find("Prompt")?.GetComponent<Text>();
        status = GameObject.Find("Status")?.GetComponent<Text>();
        Bind("RevealButton", Reveal); Bind("PreviousButton", Previous); Bind("NextButton", Next); Bind("BackButton", Back); Bind("QuitButton", Quit);
        Show();
    }
    void Bind(string name, UnityEngine.Events.UnityAction action){var go=GameObject.Find(name);if(go!=null){var button=go.GetComponent<Button>();if(button!=null){button.onClick.RemoveAllListeners();button.onClick.AddListener(action);}}}
    void Show(){var v=verses[current];if(reference!=null)reference.text=v.reference;if(prompt!=null)prompt.text=revealed?v.text:"Tap reveal to see the verse";if(status!=null)status.text="Verse "+(current+1)+" of "+verses.Length;}
    void Reveal(){revealed=!revealed;Show();} void Previous(){current=(current-1+verses.Length)%verses.Length;revealed=false;Show();} void Next(){current=(current+1)%verses.Length;revealed=false;Show();} void Back(){SceneManager.LoadScene("MaimMenu");} void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
#else
        Application.Quit();
#endif
    }
}
