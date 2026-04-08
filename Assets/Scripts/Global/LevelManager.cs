using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private StatsDataSO _npcData;
    [SerializeField] private string _sceneToLoad = "Gameplay";
    [SerializeField] private List<NpcController> _npcList = new List<NpcController>();

    public int _enemies = 0;
    private void Start()
    {
        foreach (NpcController npc in _npcList)
        {
            if (npc.isEnemy)
                _enemies++;
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