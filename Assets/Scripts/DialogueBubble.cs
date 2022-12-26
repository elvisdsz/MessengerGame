using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueBubble : MonoBehaviour
{
    [SerializeField] private SpriteRenderer speechBackgroundSprite;
    [SerializeField] private TextMeshProUGUI speechTextObject;

    private Vector2 padding = new Vector2(0.6f, 0.4f);

    private bool isTyping = false;
    private Story story = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTyping && Input.GetKeyDown(KeyCode.Space))
            NextSentence();
    }

    public void Initialize(Vector2 position, TextAsset conversationJSON)
    {
        transform.position = position;
        this.story = new Story(conversationJSON.text);
        NextSentence();
    }

    private void Speak(string speech)
    {
        speechTextObject.SetText(speech);
        speechTextObject.ForceMeshUpdate();
        Vector2 textSize = speechTextObject.GetRenderedValues(false);
        speechBackgroundSprite.size = textSize + padding;
        StartCoroutine(TypeLetters(speech));
    }

    IEnumerator TypeLetters(string text) {
        isTyping = true;
        speechTextObject.text = "";
        foreach(char letter in text.ToCharArray()) {
            speechTextObject.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        isTyping = false;
    }

    private bool NextSentence() {
        if(story == null || isTyping)
            return false;

        if(story.canContinue) {
            Speak(story.Continue());
            return true;
        }
        
        FinishConversation();

        //playerInput._.Enable();
        return false;
    }

    public void FinishConversation() {
        Destroy(gameObject, 0.02f);
    }

}
