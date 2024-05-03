using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static string state;
    public static event System.Action<string> OnStateChanged;
    
    public static string State
    {
        get => state;
        set
        {
            OnStateChanged?.Invoke(value);
            state = value;
        }
    }
    void Start()
    {
       State = "Gameplay";
    }

    
}
