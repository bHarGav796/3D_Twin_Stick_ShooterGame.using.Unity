using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    static int EnemyCount = 0;
    public delegate void OnEnemyCountUpdated(int newCount);
    public static event OnEnemyCountUpdated EnemyCountUpdated;
    // Start is called before the first frame update
    void Start()
    {
        ++EnemyCount;
        EnemyCountUpdated?.Invoke(EnemyCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        --EnemyCount;
        EnemyCountUpdated?.Invoke(EnemyCount);
        if (EnemyCount <= 0)
        {
            LevelManager.LevelFinished();
        }
    }
}
