using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

    public int scene;
    public string sceneName;
    public string sceneTo;

    private GameObject player;

    private void Start() {
        player = GameObject.Find("Player");
    }

    public static void ChangeToScene(int scene) {

        GameObject player = GameObject.Find("Player");

        if (scene == 0) {
            SceneManager.LoadScene(scene);
            player.gameObject.transform.position = new Vector2(14.1f, -5.5f);
        }
        if (scene == 1) {
            SceneManager.LoadScene(scene);
            player.gameObject.transform.position = new Vector2(8, -10);
        }
        if (scene == 2) {
            SceneManager.LoadScene(scene);
            player.gameObject.transform.position = new Vector2(8f, -11.8f);
        }
        if (scene == 3) {
            SceneManager.LoadScene(scene);
            player.gameObject.transform.position = new Vector2(2, -3);
        }
        if (scene == 4) {
            SceneManager.LoadScene(scene);
            player.gameObject.transform.position = new Vector2(14.1f, -5.5f);
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
