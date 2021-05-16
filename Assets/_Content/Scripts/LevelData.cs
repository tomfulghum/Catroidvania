using UnityEngine;

[CreateAssetMenu(menuName = "Catroidvania/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] Vector2 offset;
    [SerializeField] Vector2 spawnPosition;
    [SerializeField] GameObject spawnCat;

    public int Id => id;
    public Vector2 Offset => offset;
    public Vector2 SpawnPosition => spawnPosition;
    public GameObject SpawnCat => spawnCat;
}
