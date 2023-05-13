using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public int participantsCount { get { return participants.Count; } }
    [SerializeField] string teamName;
    [SerializeField] List<TeamParticipant> participants = new List<TeamParticipant>();
    private void Start()
    {
        if (participants.Count > 0)
        {
            foreach (TeamParticipant participant in participants)
            {
                participant.SetTeam(this);
            }
        }
    }
    public void AssignToTeam(TeamParticipant participant)
    {
        if (participant.currentTeam != null)
        {
            participant.SetTeam(null);
        }
        participants.Add(participant);
        participant.SetTeam(this);
    }
    public void Deassign(TeamParticipant participant)
    {
        if (participant.currentTeam == this)
        {
            participants.Remove(participant);
            participant.SetTeam(null);
        }
    }
    public TeamParticipant GetNearestTeamParticipant(Vector3 position, float minRange = float.PositiveInfinity)
    {
        TeamParticipant result = null;
        foreach (TeamParticipant p in participants)
        {
            float magnitude = (p.transform.position - position).magnitude;
            if (magnitude < minRange)
            {
                minRange = magnitude;
                result = p;
            }
        }
        return result;
    }
}
