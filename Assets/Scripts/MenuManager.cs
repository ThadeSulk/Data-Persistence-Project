using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (MenuManager.Instance != null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


}
