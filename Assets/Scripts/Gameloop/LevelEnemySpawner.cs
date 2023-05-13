using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject ghostEnemyPrefab;
    [SerializeField] private GameObject spawnParticlesPrefab;
    [SerializeField] private int maxGhostEnemiesCount;
    private LevelInstance currentLevelInstance;
    
    public void SetLevel(LevelInstance inst)
    {
        inst.StartCoroutine(LevelSpawnCoroutine());
        currentLevelInstance = inst;
    }
    public IEnumerator LevelSpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            SpawnGhost(currentLevelInstance.randomSpawner.position);
        }
    }
    public void SpawnGhost(Vector3 position)
    {
        Instantiate(spawnParticlesPrefab, position, Quaternion.identity);
        currentLevelInstance.AddObjectToLevel(Instantiate(ghostEnemyPrefab, position, Quaternion.identity));
    }
}
