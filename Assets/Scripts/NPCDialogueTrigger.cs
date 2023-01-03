using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public string characterId;
    public string characterName;
    public TextAsset inkJSON;
    private DialogueSystem dialogueSystem;

    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem = DialogueSystem._instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        dialogueSystem.StartConversation(characterId, characterName, transform, inkJSON);
    }
}
