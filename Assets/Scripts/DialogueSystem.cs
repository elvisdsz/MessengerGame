using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.Linq;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem _instance;

    public Dictionary<string, DialogueBubble> currentConvs = new Dictionary<string, DialogueBubble>();

    private Transform playerTransform;

    [SerializeField] private GameObject dialogueBubblePrefab;
    [SerializeField] private GameObject eventSystemPrefab;

    private bool conversationOn = false;
    private PlayerController playerController;


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
        if(currentConvs.Count>0 || currentConvs.ContainsKey(characterId)) {// forced to run max 1 conv at a time
            Debug.Log("Already running dialogue -- "+conversationOn+"  -- "+currentConvs.Count);
            return;
        }

        DialogueBubble dialogueBubble = Instantiate(dialogueBubblePrefab).GetComponent<DialogueBubble>();
        Story story = new Story(convJSON.text);
        StartListening(story);
        currentConvs.Add(characterId, dialogueBubble);
        try {
            dialogueBubble.Initialize(characterId, characterName, npcTransform, playerTransform, story);
        } catch {
            currentConvs.Remove(characterId);
        }
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

    public void ClearDialogues() {
        foreach(KeyValuePair<string, DialogueBubble> conv in currentConvs) {
            try {
                EndConversation(conv.Key);
            } catch {}
        }
        currentConvs.Clear();
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
        TakeAction(flag, ((Ink.Runtime.IntValue) value).value);
    }

    private void LoadFlagsToStory(Story story) {
        foreach(KeyValuePair<NarrativeEngine.Flag, int> flag in NarrativeEngine.GetAllUsedFlags()) {
            story.variablesState.SetGlobal(flag.Key.ToString(), Ink.Runtime.IntValue.Create(flag.Value));
        }
    }

    private static void TakeAction(NarrativeEngine.Flag flag, int value) {
        if(NarrativeGuide._instance != null)
            NarrativeGuide._instance.SetInterestTransform(null);
        
        if(flag == NarrativeEngine.Flag.MET_KING){
            if(NarrativeEngine.GetFlag(NarrativeEngine.Flag.MET_COMMANDER)<0) {
                GameObject commander = GameObject.Find("Commander");
                if(commander != null)
                    //NarrativeGuide._instance.gameObject.SetActive(false); // FIXME: Commander Transform
                    NarrativeGuide._instance.SetInterestTransform(commander.transform);
            } else if(NarrativeEngine.GetFlag(NarrativeEngine.Flag.MET_COMPANION)==-1) {
                GameObject steve = GameObject.Find("Steve");
                if(steve != null)
                    //NarrativeGuide._instance.gameObject.SetActive(false); // FIXME: Steve Transform
                    NarrativeGuide._instance.SetInterestTransform(steve.transform);
            }
        } else if(flag == NarrativeEngine.Flag.MET_COMMANDER){
            if(NarrativeEngine.GetFlag(NarrativeEngine.Flag.MET_KING)<0) {
                GameObject king = GameObject.Find("King");
                if(king != null)
                    //NarrativeGuide._instance.gameObject.SetActive(false); // FIXME: Commander Transform
                    NarrativeGuide._instance.SetInterestTransform(king.transform);
            } else if(NarrativeEngine.GetFlag(NarrativeEngine.Flag.MET_COMPANION)==-1) {
                GameObject steve = GameObject.Find("Steve");
                if(steve != null)
                    //NarrativeGuide._instance.gameObject.SetActive(false); // FIXME: Steve Transform
                    NarrativeGuide._instance.SetInterestTransform(steve.transform);
            }
        } else if(flag == NarrativeEngine.Flag.MET_COMPANION) {

            /*
            GameObject player;
            player = GameObject.Find("Player");
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.removeGate();
            */

            GameObject steve;
            steve = GameObject.Find("Steve");
            Steve steveScript = steve.GetComponent<Steve>();
            steveScript.followPlayer = true;
            NarrativeGuide._instance.SetInterestTransform(null);
        }

        // TRANSITIONS
        else if(flag == NarrativeEngine.Flag.PLAYER_BETRAYED || flag == NarrativeEngine.Flag.COMPANION_BETRAYED) {
            if(NarrativeEngine.GetFlag(NarrativeEngine.Flag.PLAYER_BETRAYED)==0 && NarrativeEngine.GetFlag(NarrativeEngine.Flag.COMPANION_BETRAYED)==0) {
                // ALL GOOD
                SceneSwitcher.ChangeToScene(2);
            } else if(NarrativeEngine.GetFlag(NarrativeEngine.Flag.PLAYER_BETRAYED)==1 && NarrativeEngine.GetFlag(NarrativeEngine.Flag.COMPANION_BETRAYED)==0) {
                // Steve betrayed player
                SceneSwitcher.ChangeToScene(3);
            } else if(NarrativeEngine.GetFlag(NarrativeEngine.Flag.PLAYER_BETRAYED)==0 && NarrativeEngine.GetFlag(NarrativeEngine.Flag.COMPANION_BETRAYED)==1) {
                // Player betrayed Steve
                GameObject player;
                player = GameObject.Find("Player");
                PlayerController playerController = player.GetComponent<PlayerController>();
                playerController.removeSteve();
                SceneSwitcher.ChangeToScene(2);
            } 
        }
        else if(flag == NarrativeEngine.Flag.LOYALTY_TEST_RESULT && value>-1) {
            GameObject player;
            player = GameObject.Find("Player");
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.removeGate();
        }

        else if(flag == NarrativeEngine.Flag.INVITED_TO_MEET_ENEMY_LEADER && value == 1) {
            GameObject player;
            player = GameObject.Find("Player");
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.removeSteve();
            SceneSwitcher.ChangeToScene(6); //FIXME
        }

        else if(flag == NarrativeEngine.Flag.JOINED_ENEMY && value == 1) {
            NarrativeEngine.SetFlag(NarrativeEngine.Flag.ENDING, 1);
            SceneSwitcher.ChangeToScene(5); //FIXME
        }

        else if(flag == NarrativeEngine.Flag.JOINED_ENEMY && value == 0) {
            SceneSwitcher.ChangeToScene(0);
        }

        else if(flag == NarrativeEngine.Flag.COMMANDER_INFORMED && (value==1 || value==2)) {
            if(value == 1) {
                NarrativeEngine.SetFlag(NarrativeEngine.Flag.ENDING, 3);
                SceneSwitcher.ChangeToScene(5); //FIXME
            } else {
                NarrativeEngine.SetFlag(NarrativeEngine.Flag.ENDING, 2);
                SceneSwitcher.ChangeToScene(5); //FIXME
            }
        }

        else if(flag == NarrativeEngine.Flag.KING_ESCAPED) {
            if(value == 1) {
                _instance.ClearDialogues();
                GameObject.Destroy(GameObject.Find("King"));
            }

            if(NarrativeEngine.GetFlag(NarrativeEngine.Flag.COMMANDER_INFORMED)<0) {
                GameObject commander = GameObject.Find("Commander");
                if(commander != null)
                    NarrativeGuide._instance.gameObject.SetActive(commander);
            } else if(NarrativeEngine.GetFlag(NarrativeEngine.Flag.COMMANDER_INFORMED) == 0) {
                NarrativeEngine.SetFlag(NarrativeEngine.Flag.ENDING, 4);
                SceneSwitcher.ChangeToScene(5); //FIXME
            }
        }

        else if(flag == NarrativeEngine.Flag.COMMANDER_INFORMED && value==0) {
            if(NarrativeEngine.GetFlag(NarrativeEngine.Flag.KING_ESCAPED) > -1) {
                NarrativeEngine.SetFlag(NarrativeEngine.Flag.ENDING, 4);
                SceneSwitcher.ChangeToScene(5); //FIXME
            } else {
                GameObject king = GameObject.Find("King");
                if(king != null)
                    NarrativeGuide._instance.gameObject.SetActive(king);
            }
        }

    }

}
