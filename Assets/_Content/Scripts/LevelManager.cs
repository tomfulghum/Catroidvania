using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using tomfulghum.EventSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<LevelData> levels;
    [SerializeField] List<CinemachineVirtualCamera> cameras;

    GameObject activePlayer = null;

    public void LoadLevel(int id)
    {
        if (id >= levels.Count || id < 0)
            return;

        var levelData = levels[id];

        if (activePlayer)
            activePlayer.transform.position = levelData.Offset + levelData.SpawnPosition;
        else
            activePlayer = Instantiate(playerPrefab, levelData.Offset + levelData.SpawnPosition, Quaternion.identity);

        var spawnPosition = (Vector3)levelData.Offset + new Vector3(levelData.SpawnPosition.x, levelData.SpawnPosition.y, -1);
        var spawnCat = Instantiate(levelData.SpawnCat, spawnPosition, Quaternion.identity).GetComponent<CongaCat>();
        spawnCat.gameObject.layer = LayerMask.NameToLayer("Player");
        spawnCat.WillBeLeader = true;

        foreach (var cam in cameras)
            cam.gameObject.SetActive(false);
        cameras[id].gameObject.SetActive(true);
    }

    public void OnLevelFinished(int id)
    {
        LoadLevel(id + 1);
    }
}
