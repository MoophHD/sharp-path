using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeBtn : MonoBehaviour {
    private Button btn;

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(handleClick);
    }

    void handleClick()
    {
		GameActions.pause(false);	
    }
}
