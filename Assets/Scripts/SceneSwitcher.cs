using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

    public int scene;
    public string level;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            Debug.Log("Player hit");

            SceneManager.LoadScene(scene);
        }
    }
}
