using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrisonEscape : MonoBehaviour {

    public Button arrowButton;
    public Image bars;
    public int zRotate;
    public int speed;
    private float rotation;
    public GameObject escapeLayer;
    public TMP_Text text;
    public GameObject rock;

    private GameObject player;
    private PlayerController playerController;

    private void Start() {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void Update() {

        Debug.Log(playerController.pickedUpRock);

        if (!playerController.pickedUpRock) {
            Debug.Log("!pickeduprock");
            text.text = "Pick up the rock to try and escape..";
        }

        if (playerController.pickedUpRock) {
            Debug.Log("_pickeduprock");
            rock.SetActive(false);
            text.text = "Click the arrow to throw the rock at the window!";
            arrowButton.gameObject.SetActive(true);
            bars.gameObject.SetActive(true);
            playerController.pickedUpRock = false;
        }

        arrowButton.onClick.AddListener(onClick);
        rotation = arrowButton.transform.rotation.z;
        arrowButton.transform.Rotate(new Vector3(0, 0, zRotate));
        
        if (rotation <= 0.2588191) {
            zRotate = speed;
        }
        if (rotation >= 0.9659258) {
            zRotate = speed*-1;
        }
    }

    void onClick() {

        if (rotation >= 0.6087614 && rotation <= 0.7933534) {
            Debug.Log("Shot made!");
            escapeLayer.SetActive(true);
            arrowButton.gameObject.SetActive(false);
            bars.gameObject.SetActive(false);
            text.text = "You win! Travel to the window to escape!";
        } else {
            text.text = "Try again!";
        }
    }
}
