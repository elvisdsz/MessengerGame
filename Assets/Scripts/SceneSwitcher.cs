using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

    public int scene;
    public string sceneName;
    public string sceneTo;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {

            if (sceneTo.Equals("Town")) {
                PlayerSpawn.toTown = true;
                SceneManager.LoadScene(scene);
            }
            if (sceneTo.Equals("OuterTown")) {
                if (sceneName.Equals("Town")) {
                    PlayerSpawn.toOuterTown = true;
                    PlayerSpawn.fromTown = true;
                }
                if (sceneName.Equals("Camp")) {
                    PlayerSpawn.toOuterTown = true;
                    PlayerSpawn.fromCamp = true;
                }
                if (sceneName.Equals("Prison")) {
                    PlayerSpawn.toOuterTown = true;
                    PlayerSpawn.fromCamp = true; // same location
                }
                SceneManager.LoadScene(scene);
            }
            if (sceneTo.Equals("Camp")) {
                PlayerSpawn.toCamp = true;
                SceneManager.LoadScene(scene);
            }
            if (sceneTo.Equals("Prison")) {
                PlayerSpawn.toPrison = true;
                SceneManager.LoadScene(scene);
            }
        }
    }

    public static void ChangeToScene(int scene) {
        SceneManager.LoadScene(scene);
    }
}
