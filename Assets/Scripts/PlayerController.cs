using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed = 12f;
    private Vector2 movement;
    public bool atTree;
    public int mineSpeed;

    public Slider sliderMining;
    public GameObject pressP;

    private static GameObject instance;

    private void Start() {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
    }

    void Update() {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (atTree) {
            pressP.SetActive(true);
            if (Input.GetKey(KeyCode.P)) {
                pressP.SetActive(false);
                sliderMining.gameObject.SetActive(true);
            } else {
                sliderMining.gameObject.SetActive(false);
            }
        } else {
            pressP.SetActive(false);
            sliderMining.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate() {

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
