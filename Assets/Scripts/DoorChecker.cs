using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{
    public List<Door> doors;

    void Start()
    {
        
    }

    void Update()
    {
        bool flag = true;
        foreach (Door door in doors)
        {
            if (door.GetOpen() == false) 
                flag = false;
        }

        if (flag)
        {
            ///Debug.Log("close all");
            foreach (Door door in doors) door.StayOpen();
        }
            
    }
}
