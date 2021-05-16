using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    static CatType leadingCatType;

    public static CatType LeadingCatType => leadingCatType;

    void OnEnable()
    {
        CongaCat.OnLeaderCatTypeChanged += OnLeaderCatTypeChanged;
    }

    void OnDisable()
    {
        CongaCat.OnLeaderCatTypeChanged -= OnLeaderCatTypeChanged;
    }

    void OnLeaderCatTypeChanged(CatType type)
    {
        leadingCatType = type;
    }
}
