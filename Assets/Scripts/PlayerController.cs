using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed = 12f;
    private Vector2 movement;
    public bool atTree;
    public bool pickedUpRock;
    public int pickSpeed;

    public Slider sliderMining;
    public GameObject pressP;
    public GameObject canvas;
    private static GameObject instance;
    private PlayerHunger playerHunger;

    public Animator animator;

    public GameObject steve;
    public bool needSteve;


    private void Start() {

        playerHunger = gameObject.GetComponent<PlayerHunger>();

        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.tag == "Rock") {
            Debug.Log("Picked up rock");
            pickedUpRock = true;
        }
    }

    void Update() {

        if (needSteve) {
            steve.SetActive(true);
        } else {
            steve.SetActive(false);
        }

        if (NPCFruitPick.finishedFoodIntro) {
            canvas.SetActive(true);
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetInteger("babyNumber", 0);

        if (movement.x == 1) {
            animator.SetInteger("babyNumber", 3);
        }
        if (movement.x == -1) {
            animator.SetInteger("babyNumber", 2);
        }
        if (movement.y == 1) {
            animator.SetInteger("babyNumber", 4);
        }
        if (movement.y == -1) {
            animator.SetInteger("babyNumber", 1);
        }
        
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

        if (playerHunger.playerSpeed >= 0.4) {
            rb.MovePosition(rb.position + movement * (speed * playerHunger.playerSpeed) * Time.fixedDeltaTime);
        } else {
            rb.MovePosition(rb.position + movement * 0.8f * Time.fixedDeltaTime);
        }
    }
}
