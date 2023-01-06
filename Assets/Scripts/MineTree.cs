using UnityEngine;

public class MineTree : MonoBehaviour {

    private GameObject player;
    private bool atTree;
    private PlayerController playerController;
    private float mineValue;

    void Start() {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
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

                mineValue += player.GetComponent<PlayerController>().mineSpeed * Time.deltaTime;

                if (mineValue < 100) {
                    playerController.sliderMining.value = mineValue;
                } else {
                    mineValue = 0;
                    playerController.sliderMining.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                    Destroy(gameObject);
                    //player.GetComponent<PlayerController>().wood += 50;
                    //player.GetComponent<PlayerController>().treesChoppedDown++;
                }
            } else {
                mineValue = 0;
            }
        }   
    }
}
