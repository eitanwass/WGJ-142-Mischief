using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkButtonScript : MonoBehaviour
{
    public Sprite buttonPressed;
    public Sprite buttonUnpressed;
    public Button okButton;
    
    public void Start() {
        
    }
    
    public void OnClick() {
        okButton = GetComponent<Button>();
        okButton.image.sprite = buttonPressed;
    }
    
    public void OnMouseUp() {
        okButton = GetComponent<Button>();
        okButton.image.sprite = buttonUnpressed;
    }
}
