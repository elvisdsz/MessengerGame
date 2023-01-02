using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueBubble : MonoBehaviour
{
    [SerializeField] private GameObject speechObject;
    [SerializeField] private SpriteRenderer speechBackgroundSprite;
    [SerializeField] private TextMeshProUGUI speechTextObject;
    [SerializeField] private GameObject choicesObject;
    [SerializeField] private List<TextMeshProUGUI> choiceTextObjList;

    private Vector2 pivot;
    private Vector2 padding = new Vector2(0.6f, 0.4f);

    private bool isTyping = false;
    private Story story = null;

    public Vector2 testPlayerPosition = new Vector2(5f, -2f); // FIXME: remove, test-only

    // Start is called before the first frame update
    void Start()
    {
        choicesObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTyping && Input.GetKeyDown(KeyCode.Space))
            NextSentence();
    }

    public void Initialize(Vector2 pivotPosition, TextAsset conversationJSON)
    {
        pivot = pivotPosition;
        choiceTextObjList[0].transform.parent.parent.GetComponent<Canvas>().worldCamera = Camera.main;
        this.story = new Story(conversationJSON.text);
        NextSentence();
    }

    private void Speak(string speech)
    {
        speechTextObject.SetText(speech);
        speechTextObject.ForceMeshUpdate();
        Vector2 textSize = speechTextObject.GetRenderedValues(false);
        Vector2 rectSize = textSize + padding;
        speechBackgroundSprite.size = rectSize;
        speechObject.transform.position = pivot + new Vector2(0f, rectSize.y/2);
        StartCoroutine(TypeLetters(speech));
    }

    IEnumerator TypeLetters(string text) {
        isTyping = true;
        speechTextObject.text = "";
        foreach(char letter in text.ToCharArray()) {
            speechTextObject.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
        isTyping = false;
        if(story.currentChoices.Count > 0) {
            StartCoroutine(DisplayChoices(story.currentChoices));
        }
    }

    private bool NextSentence() {
        if(story == null || isTyping)
            return false;

        if(story.canContinue) {
            Speak(story.Continue());
            return true;
        } else if(story.currentChoices.Count > 0) {
            // prompt to make a choice
            return false;
        }
        
        FinishConversation();

        //playerInput._.Enable();
        return false;
    }

    public void FinishConversation() {
        Destroy(gameObject, 0.02f);
    }

    IEnumerator DisplayChoices(List<Choice> choiceList) {
        if(isTyping || choiceList == null || choiceList.Count<1)
            yield break;

        isTyping = true;
        choicesObject.SetActive(true);
        choicesObject.transform.position = testPlayerPosition;    //TODO: Move to update?
        for(int i=0; i<choiceTextObjList.Count; i++) {
            TextMeshProUGUI choiceObj = choiceTextObjList[i];

            if(i<choiceList.Count) {
                GameObject choiceParent = choiceObj.transform.parent.gameObject;
                choiceParent.SetActive(true);
                choiceObj.SetText(choiceList[i].text);
                choiceObj.ForceMeshUpdate();
                Vector2 textSize = choiceObj.GetRenderedValues(false);
                Debug.Log("Choice: "+choiceList[i].text+" == "+(textSize + padding));
                choiceParent.GetComponent<RectTransform>().sizeDelta = textSize + padding;
                //yield return new WaitForSeconds(0.2f);
            } else {
                choiceObj.text = "";
                choiceObj.transform.parent.gameObject.SetActive(false);
            }
        }
        isTyping = false;
    }

    public void OnChoiceClick(int choiceIndex) {
        if(isTyping)
            return;
            
        HideChoices();
        story.ChooseChoiceIndex(choiceIndex);
        NextSentence();
    }

    private void HideChoices() {
        for(int i=0; i<choiceTextObjList.Count; i++) {
            choiceTextObjList[i].SetText("");
            choiceTextObjList[i].transform.parent.gameObject.SetActive(false);
        }
        choicesObject.SetActive(false);
    }

}
