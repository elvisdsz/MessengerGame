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

    private string characterId;
    private Vector2 pivot;
    private Vector2 padding = new Vector2(0.6f, 0.4f);
    private Vector2 choicePadding = new Vector2(1.2f, 0.4f);
    private float maxDistanceConvCut = 3f;

    private bool isTyping = false;
    public Story story {get ; private set;} = null;

    private Transform npcTransform;
    private Transform playerTransform;

    public Vector2 testPlayerPosition = new Vector2(5f, -2f); // FIXME: remove, test-only

    // Start is called before the first frame update
    void Start()
    {
        choicesObject.SetActive(false);
        //speechObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!speechObject.activeSelf)
            return;

        if(Vector2.Distance(playerTransform.position, npcTransform.position) > maxDistanceConvCut)
            FinishConversation();

        if(!isTyping && Input.GetKeyDown(KeyCode.Space))
            NextSentence();

        if(pivot != (Vector2)npcTransform.position) {
            pivot = npcTransform.position;
            speechObject.transform.position = pivot + new Vector2(0f, (npcTransform.localScale.y*0.75f)+speechBackgroundSprite.size.y/2);
        }

        if(choicesObject.activeInHierarchy) {
            choicesObject.transform.position = (Vector2)playerTransform.position + new Vector2(0f, playerTransform.localScale.y*0.75f);
        }
    }

    public void Initialize(string characterId, Transform pivotTransform, Transform playerTransform, Story story)
    {
        this.characterId = characterId;
        this.pivot = pivotTransform.position;
        this.npcTransform = pivotTransform;
        this.playerTransform = playerTransform;
        choiceTextObjList[0].transform.parent.parent.GetComponent<Canvas>().worldCamera = Camera.main;
        this.story = story;
        speechObject.SetActive(true);
        NextSentence();
    }

    private void Speak(string speech)
    {
        speechTextObject.SetText(speech);
        speechTextObject.ForceMeshUpdate();
        Vector2 textSize = speechTextObject.GetRenderedValues(false);
        Vector2 rectSize = textSize + padding;
        speechBackgroundSprite.size = rectSize;
        speechObject.transform.position = pivot + new Vector2(0f, (npcTransform.localScale.y*0.75f)+rectSize.y/2);
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
    }

    private bool NextSentence() {
        if(story == null || isTyping)
            return false;

        if(story.canContinue) {
            Speak(story.Continue());
            return true;
        } else if(story.currentChoices.Count > 0) {
            StartCoroutine(DisplayChoices(story.currentChoices));
            return true;
        }
        
        FinishConversation();

        //playerInput._.Enable();
        return false;
    }

    public void FinishConversation() {
        speechObject.SetActive(false);
        DialogueSystem._instance.EndConversation(characterId);
    }

    IEnumerator DisplayChoices(List<Choice> choiceList) {
        if(isTyping || choiceList == null || choiceList.Count<1)
            yield break;

        isTyping = true;
        choicesObject.SetActive(true);
        choicesObject.transform.position = (Vector2)playerTransform.position + new Vector2(0f, playerTransform.localScale.y*0.75f);
        for(int i=0; i<choiceTextObjList.Count; i++) {
            TextMeshProUGUI choiceObj = choiceTextObjList[i];

            if(i<choiceList.Count) {
                GameObject choiceParent = choiceObj.transform.parent.gameObject;
                choiceParent.SetActive(true);
                choiceObj.SetText(choiceList[i].text);
                choiceObj.ForceMeshUpdate();
                Vector2 textSize = choiceObj.GetRenderedValues(false);
                //Debug.Log("Choice: "+choiceList[i].text+" == "+(textSize + padding));
                choiceParent.GetComponent<RectTransform>().sizeDelta = textSize + padding;
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
