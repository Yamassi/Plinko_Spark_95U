using System.Collections.Generic;
using UnityEngine;

public class ColorSet : MonoBehaviour
{
    public static ColorSet Instance { get; private set; } = null;
    public List<Color> Colors;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}