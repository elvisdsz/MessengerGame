using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeEngine : MonoBehaviour
{
    [SerializeField] private State currentState = State.BEGIN;
    [SerializeField] private Dictionary<Flag, bool> flagValues = new Dictionary<Flag, bool>();

    public enum State {
        // ACT 1
        BEGIN, MISSION_RECEIVED, JOURNEY_STARTED,
        // ACT 2
        LOYALTY_TESTED, BETRYAL_CHOSEN, ENEMY_CAMP_REACHED,
        // ACT 3
        RETURNED_TO_TOWN,
        // ENDINGS
        TOWN_DESTROYED, TOWN_DEFEATED, TOWN_SEIZED, TOWN_SEIZED_AND_SAVED,
    }

    public enum Flag {
        MET_KING, MET_COMMANDER, MET_COMPANION,
        PLAYER_BETRAYED, COMPANION_BETRAYED,
        JOINED_ENEMY,
        LEADER_ESCAPED, COMMANDER_INFORMED1, COMMANDER_INFORMED2, 
    }

    public void SetFlag(Flag flagName, bool value) {
        flagValues.Add(flagName, value);
    }

    public bool GetFlag(Flag flagName) {
        try {
            return flagValues[flagName];
        } catch(KeyNotFoundException) {
            return false;
        }
    }

    public bool IsFlagSet(Flag flagName) {
        return flagValues.ContainsKey(flagName);
    }

    public State GetCurrentState() {
        return currentState;
    }
}
