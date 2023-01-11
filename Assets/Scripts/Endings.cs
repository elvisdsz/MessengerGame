using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class Endings : MonoBehaviour
{
    public TextAsset convJSON;
    public Animator animator;
    public TextMeshProUGUI textObj;
    private Story story;
    private bool isTyping=false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ending loadded");
        story = new Story(convJSON.text);
        LoadFlagsToStory(story);
        AudioManager.instance.StopAudio("BGM");
        if(NarrativeEngine.GetFlag(NarrativeEngine.Flag.ENDING) == 4)
            AudioManager.instance.Play("EndingGood");
        else
            AudioManager.instance.Play("EndingSad");
        //StartCoroutine(NextSentence());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isTyping -- "+isTyping);
        if(!isTyping && Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(NextSentence());
    }

    IEnumerator NextSentence() {
        if(story == null || isTyping)
            yield break;

        isTyping = true;

        if(story.canContinue) {
            string text = story.Continue();
            if(text == null || text.Length == 0)
                EndScene();

            animator.SetTrigger("FadeOut");
            yield return new WaitForSeconds(2f);
            textObj.SetText(text);
            //Debug.Log("TEXT: "+text);
            animator.SetTrigger("FadeIn");
            yield return new WaitForSeconds(2f);
            isTyping = false;
        } else {
            EndScene();
        }
    }

    public void EndScene()
    {
        Application.Quit();
    }

    private void LoadFlagsToStory(Story story) {
        foreach(KeyValuePair<NarrativeEngine.Flag, int> flag in NarrativeEngine.GetAllUsedFlags()) {
            story.variablesState.SetGlobal(flag.Key.ToString(), Ink.Runtime.IntValue.Create(flag.Value));
        }
    }
}
