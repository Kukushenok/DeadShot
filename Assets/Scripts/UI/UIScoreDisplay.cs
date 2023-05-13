using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        GameloopManager.OnScoreChange.AddListener(OnScoreChanged);
    }

    private void OnScoreChanged(int score)
    {
        scoreLabel.text = score.ToString();
    }
}
