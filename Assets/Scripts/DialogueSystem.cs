using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        dialogueBubble.Initialize(characterId, npcTransform, playerTransform, convJSON);
        currentConvs.Add(characterId, dialogueBubble);
        conversationOn = true;
    }

    public void EndConversation(string characterId) {
        Debug.Log(new List<string>(currentConvs.Keys));
        Destroy(currentConvs[characterId].gameObject, 0.02f);
        currentConvs.Remove(characterId);
        if(currentConvs.Count == 0)
            conversationOn = false;
    }

    public bool IsConversationOn() {
        return conversationOn;
    }
}
