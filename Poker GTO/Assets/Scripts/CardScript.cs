using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform startParent;
    Vector2 curPos;

	// Use this for initialization
	void Start () {
        startParent = transform.parent;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag Ended");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
        PlayerController.PlayerControllerSingle.CurrentHover = transform.parent;
        startParent = transform.parent;
        transform.SetParent(GameObject.Find("Canvas").transform);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse Exit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Mouse Up");

        curPos = eventData.position;
        //print(curPos);
        //print(PlayerController.PlayerControllerSingle.CurrentHover.name);
        if (PlayerController.PlayerControllerSingle.CurrentHover.name.Contains("UsedCard"))
        {
            //if there is already a card here
            if (PlayerController.PlayerControllerSingle.CurrentHover.childCount == 1)
            {
                //Set card in slot to clicked/dragging/current card's starting location
                PlayerController.PlayerControllerSingle.CurrentHover.GetChild(0).transform.SetParent(startParent);
            }
            //if open slot
            else
            {
                //if moving from a spot not named UsedCard
                if (!startParent.name.Contains("UsedCard"))
                {
                    //increase UsedCards count by 1
                    PlayerController.PlayerControllerSingle.incUsedCardsNumber();
                }
            }
        }
        else
        {
            //if moving from a spot named UsedCard to not named UsedCard
            if (startParent.name.Contains("UsedCard"))
            {
                //decrease UsedCards count by 1
                PlayerController.PlayerControllerSingle.decUsedCardsNumber();
            }
        }
        
        float curDis = 9999;
        int index = 0;
        if (PlayerController.PlayerControllerSingle.CurrentHover.name == "FreeCards")
        {
            //start at 1 because start button is turned off
            //if start button if removed or not set to location 0 or more objects are added then change this
            for (int x = 1; x < PlayerController.PlayerControllerSingle.CurrentHover.childCount; x++)
            {
                if (Vector2.Distance(PlayerController.PlayerControllerSingle.CurrentHover.GetChild(x).transform.position, transform.position) < curDis)
                {
                    index = x;
                    curDis = Vector2.Distance(PlayerController.PlayerControllerSingle.CurrentHover.GetChild(x).transform.position, curPos);
                    //print(PlayerController.PlayerControllerSingle.CurrentHover.GetChild(x).transform.name);
                    //print(PlayerController.PlayerControllerSingle.CurrentHover.GetChild(x).transform.position);
                }
            }

            //make sure card goes to correct side visually when dropped
            if (PlayerController.PlayerControllerSingle.CurrentHover.GetChild(index).transform.position.x - transform.position.x <= 0)
            {
                index++;
            }
        }



        //sets current cards location
        transform.SetParent(PlayerController.PlayerControllerSingle.CurrentHover);
        //this only changes position of card when other child is present

        transform.SetSiblingIndex(index);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
