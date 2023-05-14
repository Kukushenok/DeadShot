using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameloopManager gameloopManager;
    [SerializeField] private GameObject ghostEnemyPrefab;
    [SerializeField] private GameObject ghostEnemyWithRevolverPrefab;
    [SerializeField] private GameObject ghostEnemyWithSkelegunPrefab;
    [SerializeField] private GameObject spawnParticlesPrefab;
    private int maxEnemiesCount
    {
        get
        {
            return 2 + GameloopManager.singleton.passedLevelCount;
        }
    }
    private float spawnRate
    {
        get
        {
            float time = 3 - GameloopManager.singleton.passedLevelCount / 4;
            if (time < 1) time = 1;
            return time;
        }
    }
    private float enemyMaxHP
    {
        get
        {
            float currHealth = 4 + Mathf.Sin(GameloopManager.singleton.passedLevelCount * Mathf.PI / 4) * 2;
            if (currHealth > 5) currHealth = 5;
            return currHealth;
        }
    }

    public GameObject GetRandomEnemyPrefab()
    {
        float wGhost = 3 + Mathf.Pow(2 * gameloopManager.passedLevelCount, 0.5f);
        float wGhostRevolver = Mathf.Pow(2 * gameloopManager.passedLevelCount, 0.75f) - 6;
        float wGhostSkelegun = Mathf.Pow(gameloopManager.passedLevelCount, 0.33f) + gameloopManager.passedLevelCount / 4;
        if (wGhostRevolver < 0) wGhostRevolver = 0;
        if (wGhostSkelegun < 0) wGhostSkelegun = 0;
        float sumWeight = Random.Range(0, wGhost + wGhostRevolver + wGhostSkelegun);
        if (sumWeight < wGhost) return ghostEnemyPrefab;
        sumWeight -= wGhost;
        if (sumWeight < wGhostRevolver) return ghostEnemyWithRevolverPrefab;
        return ghostEnemyWithSkelegunPrefab;
    }
    public void EnableSomeLamps(LevelInstance inst)
    {
        List<LampEnemy> lamps = new List<LampEnemy>(inst.lamps);
        int lampCount = gameloopManager.passedLevelCount/2 - 2;
        if (lampCount <= 0) return;
        for (int i = 0; i < lampCount; i++)
        {
            if (lamps.Count == 0) break;
            int randomIndex = Random.Range(0, lamps.Count);
            lamps[randomIndex].SetEnable(true);
            lamps.RemoveAt(randomIndex);
        }
    }
    public void SetLevel(LevelInstance inst)
    {
        inst.StartCoroutine(LevelSpawnCoroutine());
        EnableSomeLamps(inst);
    }
    public IEnumerator LevelSpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnGhost(gameloopManager.currentLevelInstance.randomSpawner.position);
        }
    }
    public void SpawnGhost(Vector3 position)
    {
        Instantiate(spawnParticlesPrefab, position, Quaternion.identity);
        TeamParticipant nearestParticipant = TeamManager.singleton.enemyTeam.GetNearestTeamParticipant(position, 0.5f);
        if (nearestParticipant != null)
        {
            InertiaMovementController.Punch(nearestParticipant.gameObject, Random.insideUnitCircle);
        }
        GameObject selectedEnemyPrefab = GetRandomEnemyPrefab();
        GameObject enemyInstance = Instantiate(selectedEnemyPrefab, position, Quaternion.identity);
        gameloopManager.currentLevelInstance.AddObjectToLevel(enemyInstance);
        Health hp = enemyInstance.GetComponent<Health>();
        if (hp != null) hp.ChangeMaxHPProportionally(enemyMaxHP);
    }
}
