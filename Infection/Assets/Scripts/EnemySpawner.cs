using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/* Spawns random enemies with random weapons */
public class EnemySpawner : MonoBehaviour
{
    /* Serialized private fields */
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();                    // An enmemy will be randomly selected from this list
    [SerializeField] private List<GameObject> weaponPrefabs = new List<GameObject>();                   // A weapon will be randomly selected from this list
    [SerializeField] private float maxTimeUntilSpawn = 10f;                                              // Range of times enemies will spawn 
    [SerializeField] private float minTimeUntilSpawn = 5f;
    
    /* Private fields */
    private GameObject _player = null;

    /* Start spawning upon start */
    private void Start()
    {
        InitializeVariables();
        StartCoroutine(WaitAndSpawn());
    }
    
    /* Coroutine to wait and spawn enemies -- only spawn enemies when player exists in scene */
    private IEnumerator WaitAndSpawn()
    {
        while (_player)
        {
            var enemyGameObject = Instantiate(ChooseEnemy(), transform.position, Quaternion.identity);
            Enemy enemy = enemyGameObject.GetComponent<Enemy>();
            enemy.EquipWeapon(ChooseWeapon());
            yield return new WaitForSeconds(Random.Range(minTimeUntilSpawn, maxTimeUntilSpawn));   
        }
    }

    /* Selects and returns a random enemy from the prefab list */
    private GameObject ChooseEnemy()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }

    /* Selects and returns a random weapon from the prefab list */
    private GameObject ChooseWeapon()
    {
        return weaponPrefabs[Random.Range(0, weaponPrefabs.Count)];
    }

    /* Stop coroutines upon destruction of this gameobject */
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    
    private void InitializeVariables()
    {
        _player = FindObjectOfType<Player>().gameObject;
    }
}
