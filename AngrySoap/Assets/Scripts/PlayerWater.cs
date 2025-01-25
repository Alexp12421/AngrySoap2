using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWater : MonoBehaviour
{
    [SerializeField]
    private int water = 100;

    [SerializeField]
    private bool hasWater = true;

    [SerializeField]
    GameObject moistureBar;

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
            moistureBar.transform.rotation = Quaternion.Euler(0, 0,-90 - 1.8f * water);
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
