using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameloopManager : MonoBehaviour
{
    private delegate void MidTransitionEvent();
    public static GameloopManager singleton { get; private set; }
    public static UnityEvent<int> OnScoreChange = new UnityEvent<int>();
    public static Character mainCharacter { get { return singleton.character; } }
    public int currentScore { get; private set; }
    public int passedLevelCount { get; private set; }

    public int maxLevelScore { get { return (passedLevelCount + 1) * 1; } }
    [SerializeField] private Character character;
    [SerializeField] private List<GameObject> allMaps;
    [SerializeField] private CameraLurker lurkingCamera;
    [SerializeField] private LevelEnemySpawner enemySpawner;
    public LevelInstance currentLevelInstance { get; private set; }
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            passedLevelCount = 0;
            currentScore = 0;
            LoadLevel(0);
        }
    }
    public void AddOneScore()
    {
        if (currentScore >= maxLevelScore) return;
        currentScore++;
        OnScoreChange.Invoke(currentScore);
    }
    private void LoadLevel(int levelIdx)
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
    public void NextLevel()
    {
        Debug.Log("sample");
        passedLevelCount++;
        OnScoreChange.Invoke(currentScore);
        Time.timeScale = 0;
    }
    private IEnumerator Transition(MidTransitionEvent toCall)
    {
        yield return new WaitForSeconds(10);
    }
}
