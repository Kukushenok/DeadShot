using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager singleton;
    public Team playerTeam;
    public Team enemyTeam;
    public void Start()
    {
        if (singleton == null) singleton = this;
    }
    public static bool IsInTheSameTeam(GameObject one, GameObject two)
    {
        TeamParticipant pOne, pTwo;
        pOne = one.GetComponent<TeamParticipant>();
        pTwo = two.GetComponent<TeamParticipant>();
        if (pOne == null || pTwo == null) return false;
        return pOne.currentTeam == pTwo.currentTeam;
    }
    public static TeamParticipant GetPlayer(Vector3 position, float maxRange = float.PositiveInfinity)
    {
        return singleton.playerTeam.GetNearestTeamParticipant(position, maxRange);
    }
}
