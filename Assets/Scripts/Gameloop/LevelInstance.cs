using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInstance : MonoBehaviour
{
    public List<LampEnemy> lamps { get { return _lamps; } }
    public List<Transform> enemySpawners { get { return _enemySpawners; } }
    public Transform cameraRectEnclosure { get { return _cameraRectEnclosure; } }
    [SerializeField] private List<Transform> _enemySpawners;
    [SerializeField] private Transform _cameraRectEnclosure;
    [SerializeField] private List<LampEnemy> _lamps;
}
