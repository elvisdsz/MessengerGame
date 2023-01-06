using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeEngine : MonoBehaviour
{
    [SerializeField] private static State currentState = State.BEGIN;
    [SerializeField] private static Dictionary<Flag, int> flagValues = new Dictionary<Flag, int>();

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
        TEST_FLAG,
        MET_KING, MET_COMMANDER, MET_COMPANION,
        PLAYER_BETRAYED, COMPANION_BETRAYED,
        JOINED_ENEMY,
        KING_ESCAPED, COMMANDER_INFORMED1, COMMANDER_INFORMED2, 
    }

    public static void SetFlag(Flag flagName, int value) {
        flagValues[flagName] = value;
    }

    public static int GetFlag(Flag flagName) {
        try {
            return flagValues[flagName];
        } catch(KeyNotFoundException) {
            return -1;
        }
    }

    public static bool IsFlagSet(Flag flagName) {
        return flagValues.ContainsKey(flagName);
    }

    public static State GetCurrentState() {
        return currentState;
    }

    public static Dictionary<Flag, int> GetAllUsedFlags() {
        return flagValues;
    }

    public static void ResetNarrative() {
        currentState = State.BEGIN;
        flagValues.Clear();
    }
}
