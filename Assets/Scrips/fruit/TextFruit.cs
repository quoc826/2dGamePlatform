using System.Collections;
using TMPro;
using UnityEngine;

public class TextFruit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI textFruit;


    void Start()
    {
        StartCoroutine(UpdateFruit());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator UpdateFruit()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            textFruit.text = gameManager.Instance.GetFruit().ToString();
        }
    }
}
