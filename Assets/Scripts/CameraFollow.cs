using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float followSpeed;
    public float yOffset = 1f;
    public Transform target;

    private static GameObject instance;

    private void Start() {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
        target = GameObject.Find("Player").transform;
    }

    void Update() {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
