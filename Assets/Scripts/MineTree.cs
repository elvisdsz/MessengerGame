using UnityEngine;

public class MineTree : MonoBehaviour {

    private GameObject player;
    private bool atTree;
    private float mineValue;

    private PlayerController playerController;
    private PlayerHunger playerHunger;

    private float timer;
    private bool wait;


    void Start() {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerHunger = player.GetComponent<PlayerHunger>();
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.name == "Player") {
            atTree = true;
            playerController.atTree = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        
        if (collision.gameObject.name == "Player") {
            atTree = false;
            playerController.atTree = false;
        }
    }

    void Update() {

        if (atTree) {

            if (Input.GetKey(KeyCode.P)) {

                mineValue += player.GetComponent<PlayerController>().pickSpeed * Time.deltaTime;

                if (mineValue < 100) {
                    playerController.sliderMining.value = mineValue;
                } else {
                    mineValue = 0;
                    playerHunger.foodSlider.value += 25;

                    //wait = true;
                    //playerController.wait = true;
                }
            } else {
                mineValue = 0;
            }
        }

        // to add timer
        /*
        if (wait) { 
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= 5) {
                wait = false;
                playerController.wait = false;
                timer = 0f;
            }
        } else {
            if (atTree) {

                if (Input.GetKey(KeyCode.P)) {

                    mineValue += player.GetComponent<PlayerController>().mineSpeed * Time.deltaTime;

                    if (mineValue < 100) {
                        playerController.sliderMining.value = mineValue;
                    } else {
                        mineValue = 0;
                        playerHunger.foodSlider.value += 10;
                        wait = true;
                        playerController.wait = true;
                    }
                } else {
                    mineValue = 0;
                }
            } 
        } 
        */
    }
}
