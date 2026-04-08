using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private StatsDataSO _npcData;
    [SerializeField] private string _sceneToLoad = "Gameplay";
    [SerializeField] private int _minEnemies = 5;
    [SerializeField] private int _maxEnemies = 14;
    [SerializeField] private List<NpcController> _npcList = new List<NpcController>();

    public int _enemies = 0;
    private void Start()
    {
        int rand = Random.Range(_minEnemies, (_maxEnemies + 1));
        foreach (NpcController npc in _npcList)
        {
            if (npc.isEnemy)
                _enemies++;
        }
        _npcList.Sort();
        for (int i = 0; i < rand; i++)
        {
            _npcList[i].isEnemy = true;
        }

    }

    private void OnEnable()
    {
        NpcHealthSystem.OnNpcDie += OnNpcDie_CheckForWin;
    }

    private void OnDisable()
    {
        NpcHealthSystem.OnNpcDie -= OnNpcDie_CheckForWin;
    }

    private void OnNpcDie_CheckForWin(bool isEnemy)
    {
        if (isEnemy)
            _enemies--;

        if (_enemies <= 0)
            BuffEnemiesAndReload();
    }

    private void BuffEnemiesAndReload()
    {
        _npcData.level++;
        SceneManager.LoadScene(_sceneToLoad);
    }
}