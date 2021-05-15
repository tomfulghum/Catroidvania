using UnityEngine;

[CreateAssetMenu(menuName = "Catroidvania/Cat Dictionary")]
public class CatDictionary : ScriptableObject
{
    [SerializeField] GameObject sowaCatPrefab;
    [SerializeField] GameObject chonkCatPrefab;
    [SerializeField] GameObject smallCatPrefab;
    [SerializeField] GameObject pushCatPrefab;

    public GameObject GetCatPrefab(CatType type)
    {
        switch(type) {
            case CatType.Sowa:
                return sowaCatPrefab;
            case CatType.Chonk:
                return chonkCatPrefab;
            case CatType.Small:
                return smallCatPrefab;
            case CatType.Push:
                return pushCatPrefab;
            default:
                return null;
        }
    }
}
