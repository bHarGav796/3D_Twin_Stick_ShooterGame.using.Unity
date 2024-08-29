using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI AmtText;
    private void Awake()
    {
        EnemyCounter.EnemyCountUpdated += UpdateEnemyCount;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateEnemyCount(int enemyCount)
    {
        AmtText.SetText(enemyCount.ToString());
    }
}
