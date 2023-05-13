using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameloopManager gameloopManager;
    [SerializeField] private GameObject ghostEnemyPrefab;
    [SerializeField] private GameObject ghostEnemyWithWeaponPrefab;
    [SerializeField] private GameObject spawnParticlesPrefab;
    [SerializeField] private int maxGhostEnemiesCount;
    
    public void SetLevel(LevelInstance inst)
    {
        inst.StartCoroutine(LevelSpawnCoroutine());
    }
    public IEnumerator LevelSpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            SpawnGhost(gameloopManager.currentLevelInstance.randomSpawner.position);
        }
    }
    public void SpawnGhost(Vector3 position)
    {
        Instantiate(spawnParticlesPrefab, position, Quaternion.identity);
        TeamParticipant nearestParticipant = TeamManager.singleton.enemyTeam.GetNearestTeamParticipant(position, 0.5f);
        if(nearestParticipant != null)
        {
            InertiaMovementController.Punch(nearestParticipant.gameObject, Random.insideUnitCircle);
        }
        gameloopManager.currentLevelInstance.AddObjectToLevel(Instantiate(ghostEnemyPrefab, position, Quaternion.identity));
    }
}
