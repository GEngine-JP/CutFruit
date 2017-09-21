using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;

public class UIOver : MonoBehaviour {

    public UnityEvent events;
	
    public void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
