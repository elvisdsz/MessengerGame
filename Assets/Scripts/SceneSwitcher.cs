using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

    public int scene;
    public string sceneName;
    public string sceneTo;

    private GameObject player;
    private PlayerController playerController;

    private void Start() {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    public static void ChangeToScene(int scene) {

        GameObject player = GameObject.Find("Player");
        PlayerController playerController;
        playerController = player.GetComponent<PlayerController>();

        // Town
        if (scene == 0) {
            SceneManager.LoadScene(scene);
            player.gameObject.transform.position = new Vector2(14.1f, -5.5f);
            //DialogueSystem.ClearDialogues();
        }
        // OuterTown
        if (scene == 1) {
            SceneManager.LoadScene(scene);
            player.gameObject.transform.position = new Vector2(8, -10);
        }
        // Camp
        if (scene == 2) {
            SceneManager.LoadScene(scene);
            player.gameObject.transform.position = new Vector2(8f, -9f);
            DialogueSystem._instance.ClearDialogues();
        }
        // Prison
        if (scene == 3) {
            SceneManager.LoadScene(scene);
            playerController.removeSteve();
            player.gameObject.transform.position = new Vector2(2, -3);
        }
        // Fire Town
        if (scene == 4) {
            SceneManager.LoadScene(scene);
            player.gameObject.transform.position = new Vector2(14.1f, -5.5f);
        }
        // Endings
        if (scene == 5) {
            SceneManager.LoadScene(scene);
            //player.gameObject.transform.position = new Vector2(14.1f, -5.5f);
        }
        // Elvis camp
        if (scene == 6) {
            SceneManager.LoadScene(scene);
            player.gameObject.transform.position = new Vector2(8f, -9f);
            DialogueSystem._instance.ClearDialogues();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {

            if (sceneTo.Equals("Town")) {
                //PlayerSpawn.toTown = true;
                player.gameObject.transform.position = new Vector2(14.1f, -5.5f);
                SceneManager.LoadScene(scene);
            }
            if (sceneTo.Equals("OuterTown")) {
                player.gameObject.transform.position = new Vector2(8, -10);
                SceneManager.LoadScene(scene);
            }
            if (sceneTo.Equals("Camp")) {
                //PlayerSpawn.toCamp = true;
                player.gameObject.transform.position = new Vector2(8f, -11.8f);
                SceneManager.LoadScene(scene);
            }
            if (sceneTo.Equals("Prison")) {
                //PlayerSpawn.toPrison = true;
                player.gameObject.transform.position = new Vector2(2, -3);
                SceneManager.LoadScene(scene);
            }
        }
    }
}
