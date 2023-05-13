using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameloopManager : MonoBehaviour
{
    public int passedLevelCount { get; private set; }
    [SerializeField] private Character character;
    [SerializeField] private List<GameObject> allMaps;
    [SerializeField] private CameraLurker lurkingCamera;
    [SerializeField] private LevelEnemySpawner enemySpawner;
    [SerializeField] private LevelInstance currentLevelInstance;
    public void Start()
    {
        passedLevelCount = 0;
        LoadLevel(0);
    }
    public void LoadLevel(int levelIdx)
    {
        if (currentLevelInstance != null)
        {
            currentLevelInstance.Unload();
        }
        LevelInstance inst = Instantiate(allMaps[levelIdx]).GetComponent<LevelInstance>();
        enemySpawner.SetLevel(inst);
        lurkingCamera.SetFittingRect(inst.cameraRectEnclosure);
        currentLevelInstance = inst;
    }
    
}
