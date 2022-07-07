using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private bool _playerInZone = false;
    private bool _hasSpawned = true;
    private SeasonManagerScript.Season _season = SeasonManagerScript.Season.SPRING;
    private Transform _player;

    //public BoxCollider2D _col;
    public SeasonManagerScript seasonManager;
    public List<GameObject> enemyPrefabs;
    public List<Transform> spawnPositions;
    public SeasonManagerScript.Season spawnSeason = SeasonManagerScript.Season.SPRING;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (_playerInZone)
        {
            _season = seasonManager.GetSeason();

            if (!_hasSpawned && _season == spawnSeason)
            {
                //Debug.Log("spawn enemy");
                for (int i = 0; i < enemyPrefabs.Count; i++)
                {
                    GameObject e = Instantiate(enemyPrefabs[i]);
                    e.GetComponent<Enemy>().SetPlayer(_player);
                    e.transform.position = spawnPositions[i].position;
                }
                _hasSpawned = true;
            }
            else if (_hasSpawned && _season != spawnSeason)
            {
                _hasSpawned = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            _playerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            _playerInZone = false;
    }
}
