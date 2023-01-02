using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float followSpeed;
    public float yOffset = 1f;
    public Transform target;

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
