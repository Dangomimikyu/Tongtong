using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;
using UnityEngine.SceneManagement;

public class UnitSpawner : MonoBehaviour
{
    [Header("object references")]
    [SerializeField]
    private GameObject m_unitPrefab;

    [Header("Spawning specifications")]
    [SerializeField]
    private List<Vector3> m_baseSpawnLocations;
    [SerializeField]
    private Vector3 m_baseScaleSize;
    [SerializeField]
    private List<Vector3> m_fieldSpawnLocations;
    [SerializeField]
    private Vector3 m_fieldScaleSize;

    private UnitDataManager m_unitDataManager;

    #region Monobehaviour functions
    void Start()
    {
        EventManager.instance.StartListening(GameEvents.Misc_SceneChange, SpawnUnits);
        m_unitDataManager = GetComponent<UnitDataManager>();
	}

    void Update()
    {

    }
    #endregion

    #region Spawning functions
    private void SpawnUnits(EventArgumentData ead)
	{
        // check the currently active scene
        string currentSceneName = (string)ead.eventParams[0];

        // don't need to spawn anything if it's the other scenes
        if (currentSceneName == "HomeBaseScene")
		{
            // spawn at home base locations
            for (int i = 0; i < m_unitDataManager.activeUnits.Count; ++i)
			{
                Transform tempTransform = transform;
				tempTransform.localPosition = m_baseSpawnLocations[i];
                tempTransform.rotation      = Quaternion.identity;
                tempTransform.localScale    = m_baseScaleSize;
                GameObject unit = Instantiate(m_unitPrefab, tempTransform);
                Debug.Log("spawned at: " + tempTransform.position);
			}
		}
        else if (currentSceneName == "ExpeditionScene")
		{
            // spawn at battle positions
		}
	}
    #endregion
}
