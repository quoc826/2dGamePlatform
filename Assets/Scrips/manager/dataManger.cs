using System.Data.Common;
using UnityEngine;

public class dataManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static int dataFruit
    {
        get => PlayerPrefs.GetInt(dataKey.fruitKey, 0);
        set => PlayerPrefs.SetInt(dataKey.fruitKey, value);
    }

    public static float dataMusicBackground
    {
        get => PlayerPrefs.GetFloat(dataKey.dataMusicBackground, 0.5f);
        set => PlayerPrefs.SetFloat(dataKey.dataMusicBackground, value);
    }

    public static float dataMusicMenu
    {
        get => PlayerPrefs.GetFloat(dataKey.dataMusicMenu, 0.5f);
        set => PlayerPrefs.SetFloat(dataKey.dataMusicMenu, value);
    }

    public static float dataGunSound
    {
        get => PlayerPrefs.GetFloat(dataKey.dataGunSound, 0.5f);
        set => PlayerPrefs.SetFloat(dataKey.dataGunSound, value);
    }
}
