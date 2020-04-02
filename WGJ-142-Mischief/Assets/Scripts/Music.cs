using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnValueChanged(float newValue)
    {
       Debug.Log(newValue);
        PlayerPrefs.SetFloat("Music", newValue);//saves our music volume
        Debug.Log(PlayerPrefs.GetFloat("Music"));
    }
}
