using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatus : ScriptableObject
{
    public int MaxHP;
    public int Atk;
    private int CurrentHP;

    private void OnEnable()
    {
        CurrentHP = MaxHP;
    }

    public void HPInput(int x)
    {
        CurrentHP = x;
    }
    
    public int MaxHPOutput()
    {
        return MaxHP;
    }
    public int HPOutput()
    {
        return CurrentHP;
    }
    
    public int AtkOutput()
    {
        return Atk;
    }


}