using UnityEngine;


public class Steve : MonoBehaviour {

    private GameObject player;
    public Rigidbody2D rb;
    private Vector2 playerPos;
    private Vector2 stevePos;
    private float diffX;
    private float diffY;
    private bool atPlayer;
    public bool followPlayer;
    public float speed;

    void Start() {
        player = GameObject.Find("Player");
    }

    void Update() {

        playerPos = player.transform.position;
        stevePos = gameObject.transform.position;
        diffX = playerPos.x - stevePos.x;
        diffY = playerPos.y - stevePos.y;

        if ((-1 < diffX && diffX < 1) && (-1 < diffY && diffY < 1)) {
            atPlayer = true;
        } else {
            atPlayer = false;
        }

        if (followPlayer && !atPlayer) {

            switch (playerPos.x < stevePos.x, playerPos.y < stevePos.y) {
                case (true, true):
                    stevePos = new Vector2(stevePos.x - speed / 1.41f, stevePos.y - speed / 1.41f);
                    break;
                case (true, false):
                    stevePos = new Vector2(stevePos.x - speed / 1.41f, stevePos.y + speed / 1.41f);
                    break;
                case (false, true):
                    stevePos = new Vector2(stevePos.x + speed / 1.41f, stevePos.y - speed / 1.41f);
                    break;
                case (false, false):
                    stevePos = new Vector2(stevePos.x + speed / 1.41f, stevePos.y + speed / 1.41f);
                    break;
            }
            this.transform.position = stevePos;
        }
    }
}
