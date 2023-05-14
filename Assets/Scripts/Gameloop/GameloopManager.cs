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
    public static bool isLevelCompleted { get { return singleton.currentScore >= singleton.maxLevelScore; } }
    public int maxLevelScore { get { return (passedLevelCount + 1) * 5 ; } }
    private int currentMapIndex;
    [SerializeField] private Character character;
    [SerializeField] private List<GameObject> allMaps;
    [SerializeField] private CameraLurker lurkingCamera;
    [SerializeField] private LevelEnemySpawner enemySpawner;
    [SerializeField] private int mainMenuSceneIdx;
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
        if (isLevelCompleted) return;
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
        currentLevelInstance = inst;
        currentMapIndex = levelIdx;
        enemySpawner.LevelUpdated();
        lurkingCamera.SetFittingRect(inst.cameraRectEnclosure);
    }
    public void NextLevel()
    {
        passedLevelCount++;
        OnScoreChange.Invoke(currentScore);
        TransitionManager.MakeTransition(TransferToNextLevel);
    }
    public IEnumerator TransferToNextLevel(TransitionManager.TransitionState state)
    {
        if (state == TransitionManager.TransitionState.Start)
        {
            Time.timeScale = 0;
        }
        else if (state == TransitionManager.TransitionState.End)
        {
            Time.timeScale = 1;
        }
        else
        {
            int nextMapIndex = currentMapIndex + Random.Range(1, allMaps.Count);
            LoadLevel(nextMapIndex % allMaps.Count);
            character.myHP.Heal();
            yield return new WaitForSecondsRealtime(1);
        }
    }
    public void OnCharacterDeath()
    {
        SceneLoadTransitions.LoadScene(SceneLoadTransitions.SCENE_MAINMENU);
    }
}
