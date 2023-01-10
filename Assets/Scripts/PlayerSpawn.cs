using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    public static bool fromTown;
    public static bool fromOuterTown;
    public static bool fromCamp;
    public static bool fromPrison;

    public static bool toTown;
    public static bool toOuterTown;
    public static bool toCamp;
    public static bool toPrison;

    void Update() {

        if (toTown) {
            this.gameObject.transform.position = new Vector2(14.1f, -5.5f);
            toTown = false;
        }
        if (toOuterTown) {
            this.gameObject.transform.position = new Vector2(8, -10);
            toOuterTown = false;
            fromTown = false;
        }
        if (toCamp) {
            this.gameObject.transform.position = new Vector2(8f, -11.8f);
            toCamp = false;
        }
        if (toPrison) {
            this.gameObject.transform.position = new Vector2(2, -3);
            toPrison = false;
        }
    }
}
