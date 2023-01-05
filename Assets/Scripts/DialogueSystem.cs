using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem _instance;

    public Dictionary<string, DialogueBubble> currentConvs = new Dictionary<string, DialogueBubble>();

    private Transform playerTransform;

    [SerializeField] private GameObject dialogueBubblePrefab;
    [SerializeField] private GameObject eventSystemPrefab;

    private bool conversationOn = false;

    void Awake() // singleton
    {
        if(_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform; // TODO: Optimize

        if(GameObject.Find("EventSystem") == null)
            Instantiate(eventSystemPrefab);

        //StartConversation("Test Character", testConvJSON);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartConversation(string characterId, string characterName, Transform npcTransform, TextAsset convJSON) {
        if(conversationOn || currentConvs.ContainsKey(characterId)) // forced to run max 1 conv at a time
            return;

        DialogueBubble dialogueBubble = Instantiate(dialogueBubblePrefab).GetComponent<DialogueBubble>();
        Story story = new Story(convJSON.text);
        StartListening(story);
        dialogueBubble.Initialize(characterId, npcTransform, playerTransform, story);
        currentConvs.Add(characterId, dialogueBubble);
        conversationOn = true;
    }

    public void EndConversation(string characterId) {
        Story story = currentConvs[characterId].story;
        StopListening(story);
        Destroy(currentConvs[characterId].gameObject, 0.02f);
        currentConvs.Remove(characterId);
        if(currentConvs.Count == 0)
            conversationOn = false;
    }

    public bool IsConversationOn() {
        return conversationOn;
    }


    public void StartListening(Story story) {
        LoadFlagsToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story) {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    public static void VariableChanged(string name, Ink.Runtime.Object value) {
        // Debug.Log("Variable changed: "+name+" = "+value);
        NarrativeEngine.Flag flag;
        if (!NarrativeEngine.Flag.TryParse(name, out flag)) {
            Debug.LogWarning("Ink variable does not exist as a narative engine flag.");
            return;
        }
        NarrativeEngine.SetFlag(flag, ((Ink.Runtime.IntValue) value).value);
    }

    private void LoadFlagsToStory(Story story) {
        foreach(KeyValuePair<NarrativeEngine.Flag, int> flag in NarrativeEngine.GetAllUsedFlags()) {
            story.variablesState.SetGlobal(flag.Key.ToString(), Ink.Runtime.IntValue.Create(flag.Value));
        }
    }
}
