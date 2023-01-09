using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class NPCFruitPick : MonoBehaviour {

    public GameObject dialogueBox;
    public TMP_Text npcText;
    public static int textArrayIndex;
    public static bool decline;
    public static bool finishedFoodIntro;
    ArrayList textList;

    public void Start() {

        textList = new ArrayList();

        textArrayIndex = 1;
        textList.Add("Come back when you're ready!");
        textList.Add("I'm guessing you need me to teach you to pick and eat fruit?");
        textList.Add("Walk up to any tree, press P to pick the fruit. Press E to eat the fruit");
        textList.Add("If you don't eat for a while, your energy will drop and you'll slow down");
        textList.Add("The others in town will be appreciative if you bring some back. There's not much food around anymore..");
        textList.Add("See you soon!");
    }

    public void Accept() {

        if (decline) {
            decline = false;
        } else {
            int size = textList.Count;
            if (textArrayIndex + 1 == size) {
                dialogueBox.SetActive(false);
                finishedFoodIntro = true;
            } else {
                textArrayIndex += 1;
            }
        }
    }

    public void Decline() {
        decline = true;
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.name == "Player" && NPC.finishedIntro) {
            dialogueBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.name == "Player") {
            dialogueBox.SetActive(false);
        }
    }

    void Update() {

        if (decline) {
            npcText.text = textList[0].ToString();
        } else {
            npcText.text = textList[textArrayIndex].ToString();
        }
    }
}
