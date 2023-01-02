using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

    public int scene;
    public string sceneName;
    public string sceneTo;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            if (sceneName.Equals("Town")) {
                PlayerSpawn.fromTown = true;
                SceneManager.LoadScene(scene);
            }
            if (sceneName.Equals("OuterTown")) {
                PlayerSpawn.fromOuterTown = true;
                SceneManager.LoadScene(scene);
            }
            if (sceneName.Equals("Camp")) {
                PlayerSpawn.fromCamp = true;
                SceneManager.LoadScene(scene);
            }
        }
    }
}
