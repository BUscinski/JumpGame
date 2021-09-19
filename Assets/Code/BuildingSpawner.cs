using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _BuildingPrefab;
    private float _lastZSpawned = 0;
    [SerializeField] private float _spawnLength = 100;
    private float _spawnOffset = -1200;
    private float _despawnOffset = -300;
    [SerializeField] private GameObject _MainPlayer;
    private JumpingController _jumpingController;
    private bool Spawning;

    Queue<GameObject> spawnedBuildings = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _jumpingController = _MainPlayer.GetComponent(typeof(JumpingController)) as JumpingController;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_lastZSpawned + _spawnOffset < _MainPlayer.transform.position.z)
        {
            // Instantiate at position (0, 0, _lastZSpawned) and zero rotation.
            GameObject newPrefab = Instantiate(_BuildingPrefab, new Vector3(0, 0, _lastZSpawned), Quaternion.identity);
            _lastZSpawned = _lastZSpawned + _spawnLength;
            CScape.BuildingModifier buildingModifier = newPrefab.GetComponent(typeof(CScape.BuildingModifier)) as CScape.BuildingModifier;
            buildingModifier.forceUpdate = true;
            buildingModifier.colorVariation.x = Random.Range(0, 9);

            buildingModifier.colorVariation2.x = Random.Range(0, 9);
            buildingModifier.colorVariation2.y = Random.Range(0, 9);
            buildingModifier.colorVariation2.z = Random.Range(0, 9);
            buildingModifier.colorVariation2.w = Random.Range(2, 9);

            buildingModifier.colorVariation3.x = Random.Range(0, 9);
            buildingModifier.colorVariation3.y = Random.Range(0, 9);
            buildingModifier.colorVariation3.z = Random.Range(0, 9);
            buildingModifier.colorVariation3.w = Random.Range(0, 9);

            buildingModifier.colorVariation4.x = Random.Range(0, 9);
            buildingModifier.colorVariation4.y = Random.Range(0, 9);
            buildingModifier.colorVariation4.z = Random.Range(0, 9);
            buildingModifier.colorVariation4.w = Random.Range(0, 9);
            buildingModifier.lightnessFront = Random.Range(0, 9);
            buildingModifier.lightnessSide = Random.Range(0, 9);
            buildingModifier.colorVariation5.x = Random.Range(0, 9);
            buildingModifier.colorVariation5.y = Random.Range(0, 9);
            buildingModifier.floorNumber = Random.Range(28, 32);
            buildingModifier.buildingDepth = Random.Range(7, 18);
            buildingModifier.buildingWidth = Random.Range(7, 18);
            buildingModifier.AwakeCity();
            buildingModifier.forceUpdate = false;
            _jumpingController.moveSpeed = _jumpingController.moveSpeed + 0.5f;
            spawnedBuildings.Enqueue(newPrefab);
        }
        GameObject earliestSpawnedBuilding = spawnedBuildings.Peek();
        if (earliestSpawnedBuilding != null && earliestSpawnedBuilding.transform.position.z < _jumpingController.transform.position.z  + _despawnOffset)
        {
            earliestSpawnedBuilding = spawnedBuildings.Dequeue();
            Destroy(earliestSpawnedBuilding);
        }
    }
}
