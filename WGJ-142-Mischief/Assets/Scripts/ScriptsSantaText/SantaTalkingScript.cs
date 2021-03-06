using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SantaTextNamespace;
using System;

public class SantaTalkingScript : MonoBehaviour
{
    public GameObject santaText;
    public Button okButton;
    
    private float animateStep = 10f;
    private float animateDelay = 0.00000000001f;
    private float totalPixelsUp = 10f;
    private float totalPixelsDown = 50f;

    private static LinkedList<string> santaTextList; // = new LinkedList<string>(santaTextArr);
    private static LinkedListNode<string> santaTextNode; // = santaTextList.First;
    public int[] arrIndex = new int[] {0, 0};
    
    void Start() {
        // Clear text
        santaText.GetComponent<TextMeshProUGUI>().text = "";
        
        // Deactivate entire canvas
        transform.gameObject.SetActive(false);
        
        // Bring canvas down for the next time we want to show it
        float totalPixels = totalPixelsUp - totalPixelsDown;
        foreach (Transform child in transform) {
            child.position += new Vector3(0, totalPixels, 0);
        }
    }
    
    public void TextChange() {
        string text = SantaText.santaTextArr[arrIndex[0], arrIndex[1]];
        
        // No more Santa text to show for now. Hide the Santa canvas.
        if (text == null) {
            HideSantaCanvas();
        }
        // Show the current Santa text.
        else {
            transform.gameObject.SetActive(true);
            StartCoroutine(ShowText(text));
        }
        
        // Point to the next Santa text for later.
        // if (santaTextNode.Next != null) {
        //     santaTextNode = santaTextNode.Next;
        // }
        /*
        if (arrIndex[1] < SantaText.santaTextArr.GetLength(0) - 1) {
            arrIndex[1] ++;
        }
        else {
            arrIndex[0] ++;
            arrIndex[1] = 0;
        }*/
        if (SantaText.santaTextArr[arrIndex[0], arrIndex[1]] != "") {
            arrIndex[1] ++;
            Debug.Log("don't hide1");
        }
        else {
            Debug.Log("hide1");
            arrIndex[0] ++;
            arrIndex[1] = 0;
            HideSantaCanvas();
        }
        Debug.Log("done1");
    }
    
    void HideSantaCanvas() {
        StartCoroutine(AnimateCanvasHide());
    }
    
    IEnumerator AnimateCanvasHide() {
        
        // Bring canvas up
        for (float i = 0; i < totalPixelsUp; i += animateStep) {
            foreach (Transform child in transform) {
                child.position += new Vector3(0, animateStep, 0);
            }
            
            yield return new WaitForSeconds(animateDelay);
        }
        
        yield return new WaitForSeconds(0.1f);

        // Bring canvas down until hidden
        for (float i = 0; i < totalPixelsDown; i += animateStep) {
            foreach (Transform child in transform) {
                child.position += new Vector3(0, -1 * animateStep, 0);
            }
            
            yield return new WaitForSeconds(animateDelay);
        }
        
        // Clear text
        santaText.GetComponent<TextMeshProUGUI>().text = "";
        
        // Deactivate entire canvas
        transform.gameObject.SetActive(false);
        
        /*
        // Bring canvas back up for next time
        float totalPixels = totalPixelsDown - totalPixelsUp;
        foreach (Transform child in transform) {
            child.position += new Vector3(0, totalPixels, 0);
        }*/
        
    }
    
    
    // Function to make the text appear one letter at a time (as if being typed).
    IEnumerator ShowText(string fullText) {
        string currentText;
        float letterDelay; // Time between each letter (in seconds).
        string[] endSentenceArr = {"...", ".", "!", "?"};
        
        okButton.interactable = false; // Disable ok/next button while text is still being typed.
        
        for (int i = 0; i < fullText.Length; i ++) {
            currentText = fullText.Substring(0, i + 1);
            santaText.GetComponent<TextMeshProUGUI>().text = currentText;
            
            // If sentence is over, wait a bit longer.
            if (Array.Exists(endSentenceArr, element => element == Char.ToString(fullText[i]))) {
                letterDelay = 0.3f;
            }
            else {
                letterDelay = 0.02f;
            }
            yield return new WaitForSeconds(letterDelay);
        }
        okButton.interactable = true; // Re-enable ok/next button when done typing.
    }
    
    public void ShowSantaText() {
        Debug.Log("showing text");
        // Activate canvas
        transform.gameObject.SetActive(true);
        
        // Bring canvas back up
        StartCoroutine(AnimateCanvasShow());
        /*
        // Get and show text
        string text = SantaText.santaTextNode.Value;
        StartCoroutine(ShowText(text));
        
        // Point to the next Santa text for later.
        if (SantaText.santaTextNode.Next != null) {
            SantaText.santaTextNode = SantaText.santaTextNode.Next;
        }*/
    }
    
    IEnumerator AnimateCanvasShow() {
        
        // Bring canvas up more than necessary
        for (float i = 0; i < totalPixelsDown; i += animateStep) {
            foreach (Transform child in transform) {
                child.position += new Vector3(0, animateStep, 0);
            }
            
            yield return new WaitForSeconds(animateDelay);
        }
        
        yield return new WaitForSeconds(0.1f);

        // Bring canvas back down to correct position
        for (float i = 0; i < totalPixelsUp; i += animateStep) {
            foreach (Transform child in transform) {
                child.position += new Vector3(0, -1 * animateStep, 0);
            }
            
            yield return new WaitForSeconds(animateDelay);
        }
        
        // Get text
        // string[] arr = SantaText.santaTextArr[0];
        // santaTextList = new LinkedList<string>(arr);
        // arrIndex = new int[] {0, 0};
        // string text = santaTextNode.Value;
        string text = SantaText.santaTextArr[arrIndex[0], arrIndex[1]];

        // Show text
        StartCoroutine(ShowText(text));
        
        // Point to the next Santa text for later.
        // if (santaTextNode.Next != null) {
        //     santaTextNode = santaTextNode.Next;
        // }
        if (SantaText.santaTextArr[arrIndex[0], arrIndex[1]] != "") {
            arrIndex[1] ++;
            Debug.Log("don't hide");
        }
        else {
            Debug.Log("hide");
            arrIndex[0] ++;
            arrIndex[1] = 0;
            HideSantaCanvas();
        }
        Debug.Log("done");
    }
}
