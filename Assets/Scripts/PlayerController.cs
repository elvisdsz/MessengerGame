using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    //public GameObject commander;
    //public GameObject king;
    public GameObject steve;
    public bool needSteve;


    private void Start() {

        playerHunger = gameObject.GetComponent<PlayerHunger>();
        AudioManager.instance.Play("BGM");

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

    public void getSteve() {

        steve.SetActive(true);
        Vector2 playerPos = this.transform.position;

        if (NarrativeEngine.GetFlag(NarrativeEngine.Flag.MET_COMPANION) == 1) {
            if (SceneManager.GetActiveScene().name.Equals("Town")) {
                steve.transform.position = new Vector2(playerPos.x + 1, playerPos.y);
            } else {
                steve.transform.position = new Vector2(playerPos.x + 1, playerPos.y);
                Debug.Log("Steve not town");
            }
        } else {
            steve.transform.position = new Vector2(16, -9.5f);
        }
    }

    public void removeSteve() {

        steve.SetActive(false);
    }

    public void addGate(GameObject gate) {
        
        gate.SetActive(true);
    }

    public void removeGate() {
        GameObject gate = GameObject.Find("gateTown");
        Destroy(gate);
    }

    void Update() {


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
