using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamParticipant : MonoBehaviour
{
    public Team currentTeam { get; private set; }
    public void SetTeam(Team team)
    {
        currentTeam = team;
    }
    public void OnDestroy()
    {
        if (currentTeam != null)
        {
            currentTeam.Deassign(this);
        }
    }
}
