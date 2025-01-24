using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWater : MonoBehaviour
{
    [SerializeField]
    private int water = 100;

    [SerializeField]
    private bool hasWater = true;

    public void AddWater(int amount){
        if(water <= 0){
            hasWater = true;
        }
        water += amount;
    }

    public bool ConsumeWater(int amount){
        if(water>=amount)
        {
            water -= amount;
            hasWater = water > 0;
            return true;
        }
        else
        {
            hasWater = false;
            return false;
        }
    }

    public int GetWater(){
        return water;
    }

    public bool HasWater(){
        return hasWater;
    }
}
