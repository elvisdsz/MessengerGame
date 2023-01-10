using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class NPC : MonoBehaviour {

    public GameObject dialogueBox;
    public TMP_Text npcText;
    public static int textArrayIndex;
    public static int text2ArrayIndex;
    //public static bool decline;
    public static bool finishedIntro;
    private bool needFood;

    ArrayList textList;
    ArrayList textList2;

    private GameObject player;
    private PlayerHunger playerHunger;

    public void Start() {

        player = GameObject.Find("Player");
        playerHunger = player.GetComponent<PlayerHunger>();

        textList = new ArrayList();
        textArrayIndex = 1;
        textList.Add("Come back to us when you're ready!");
        textList.Add("Hello, you must be the messenger");
        textList.Add("Make yourself useful and get me some food, it'll be worth it");
        textList.Add("Travel out of town, you'll meet someone who will teach you to pick fruits from trees");
        textList.Add("See you soon!");

        textList2 = new ArrayList();
        text2ArrayIndex = 1;
        textList2.Add("We need 40 food");
        textList2.Add("Can I have some food?");
        textList2.Add("I can't believe you'd help our King..");
        textList2.Add("The King has been draining this town with resources");
        textList2.Add("I hope he vanishes!");
        textList2.Add("Anyway, I think Steve needs your help. Goodluck..");
    }

    public void Accept () {

        if (finishedIntro && NPCFruitPick.finishedFoodIntro) {
            int size = textList2.Count;
            if (text2ArrayIndex == 1) {
                if (playerHunger.food >= 40) {
                    text2ArrayIndex += 1;
                    playerHunger.food -= 40;
                    needFood = false;
                } else {
                    needFood = true;
                } 
            } else {
                if (text2ArrayIndex + 1 == size) {
                    dialogueBox.SetActive(false);
                } else {
                    text2ArrayIndex += 1;
                }
            }
        } else {
            int size = textList.Count;
            if (textArrayIndex + 1 == size) {
                finishedIntro = true;
            } else {
                textArrayIndex += 1;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.name == "Player") {
            dialogueBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.name == "Player") {
            dialogueBox.SetActive(false);
        }
    }

    void Update() {

        if (finishedIntro && NPCFruitPick.finishedFoodIntro) {
            if (needFood) {
                npcText.text = textList2[0].ToString();
            } else {
                npcText.text = textList2[text2ArrayIndex].ToString();
            }
        } else {
            npcText.text = textList[textArrayIndex].ToString();
        }
    }
}