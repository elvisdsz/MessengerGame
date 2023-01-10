using UnityEngine;
using UnityEngine.SceneManagement;

public class NarrativeGuide : MonoBehaviour {

    public static NarrativeGuide _instance;
    public Transform interestTransform;
    public Transform pointer;
    private Vector3 screenPos;
    private Vector2 onScreenPos;
    private float max;
    private Camera _camera;

    private GameObject commander;
    private GameObject npc;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance == null) {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Town")) {
            commander = GameObject.Find("Commander");
            npc = GameObject.Find("NPC");
            Debug.Log("town starts");
        }

        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name.Equals("Town")) {
            commander = GameObject.Find("Commander");
            npc = GameObject.Find("NPC");
            if (NPC.finished) {
                interestTransform = commander.transform;
            } else {
                //Debug.Log("Here");
                interestTransform = npc.transform;
            }
        }
        

        if(interestTransform == null)
            return;

        screenPos = _camera.WorldToViewportPoint(interestTransform.position); //get viewport positions
 
        if(screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1){
            pointer.gameObject.SetActive(false);
            return;
        }

        pointer.gameObject.SetActive(true);
        onScreenPos = new Vector2(screenPos.x-0.5f, screenPos.y-0.5f)*2; //2D version, new mapping
        max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y)); //get largest offset
        onScreenPos = (onScreenPos/(max*2))+new Vector2(0.5f, 0.5f); //undo mapping
        //Debug.Log(onScreenPos);
        Vector3 centre = _camera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        Vector3 ptrPos = _camera.ViewportToScreenPoint(onScreenPos);
        Vector3 directionAway = ptrPos - centre;
        pointer.position = ptrPos - (0.1f*directionAway);
        pointer.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.up, directionAway));
    }

    public void SetInterestTransform(Transform focusTransform) {
        interestTransform = focusTransform;
    }
}
