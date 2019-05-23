using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetParent : MonoBehaviour, IPointerEnterHandler {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("set parent: "+ transform.name);
        PlayerController.PlayerControllerSingle.CurrentHover = transform;
    }

}
