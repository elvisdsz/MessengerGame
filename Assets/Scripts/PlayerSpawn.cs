using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    public static bool fromTown;
    public static bool fromOuterTown;
    public static bool fromCamp;

    public GameObject player;

    void Update() {
        
        if (fromTown) {
            this.gameObject.transform.position = new Vector2(8, -10);
            fromTown = false;
        }
        if (fromOuterTown) {
            //this.gameObject.transform.position = new Vector2();
            fromOuterTown = false;
        }
        if (fromCamp) {
            //this.gameObject.transform.position = new Vector2();
            fromCamp = false;
        }
    }
}
