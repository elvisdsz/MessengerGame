using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem _instance;

    public TextAsset testConvJSON;

    [SerializeField] private GameObject dialogueBubblePrefab;

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
        StartConversation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartConversation() {
        DialogueBubble dialogueBubble = Instantiate(dialogueBubblePrefab).GetComponent<DialogueBubble>();
        dialogueBubble.Initialize(new Vector2(0f, 0f), testConvJSON);
    }
}
