using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaTextNamespace {
    public class SantaText : MonoBehaviour
    {
        private static string[] santaTextArr = {
            "Ho, Ho, Ho! I seem to have misplaced some of the Nice kids' presents in the Naughty kids' houses. I need your help to save Christmas! Are you up to the task?",
            null,
            "Sample Santa Text 1a",
            "Sample Santa Text 1b",
            null,
            "Sample Santa Text 3a",
            "Sample Santa Text 3b",
            "Sample Santa Text 3c",
        };
        
        private static LinkedList<string> santaTextList = new LinkedList<string>(santaTextArr);
        public static LinkedListNode<string> santaTextNode = santaTextList.First;
    }
}
