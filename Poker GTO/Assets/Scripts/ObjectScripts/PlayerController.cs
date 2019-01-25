using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //
    public GameObject SubmitButton;
    public GameObject ResetButton;
    public GameObject UsedCard1;
    public GameObject UsedCard2;
    public GameObject UsedCard3;
    public GameObject UsedCard4;
    public GameObject UsedCard5;
    public GameObject UsedCard6;
    public GameObject UsedCard7;

    public Transform CurrentHover;
    public Transform FreeCards;

    public static PlayerController PlayerControllerSingle;

    //game variables
    public int UsedCardsNumber = 0;
    public bool placeCardStage = true;

    void Awake()
    {
        if (PlayerControllerSingle == null)
        {
            PlayerControllerSingle = this;
        }
        else if (PlayerControllerSingle != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void incUsedCardsNumber()
    {
        UsedCardsNumber += 1;
        if(UsedCardsNumber == 7)
        {
            SubmitButton.SetActive(true);
        }
    }

    public void decUsedCardsNumber()
    {
        UsedCardsNumber -= 1;
        if (UsedCardsNumber < 7)
        {
            SubmitButton.SetActive(false);
        }
    }

    public void resetUsedCardsNumber()
    {
        UsedCardsNumber = 0;
        SubmitButton.SetActive(false);

        //why not work? gets UsedCard2 transform not child
        //UsedCard2.GetComponentInChildren<Transform>().SetParent(FreeCards);
        //UsedCard3.GetComponentInChildren<Transform>().SetParent(FreeCards);
        //UsedCard4.GetComponentInChildren<Transform>().SetParent(FreeCards);
        //UsedCard5.GetComponentInChildren<Transform>().SetParent(FreeCards);
        //UsedCard6.GetComponentInChildren<Transform>().SetParent(FreeCards);
        //UsedCard7.GetComponentInChildren<Transform>().SetParent(FreeCards);

        //have to check if child before you index and reset. Why does above code not work?
        if (UsedCard1.transform.childCount > 0)
        {
            UsedCard1.transform.GetChild(0).transform.SetParent(FreeCards);
        }
        if (UsedCard2.transform.childCount > 0)
        {
            UsedCard2.transform.GetChild(0).transform.SetParent(FreeCards);
        }
        if (UsedCard3.transform.childCount > 0)
        {
            UsedCard3.transform.GetChild(0).transform.SetParent(FreeCards);
        }
        if (UsedCard4.transform.childCount > 0)
        {
            UsedCard4.transform.GetChild(0).transform.SetParent(FreeCards);
        }
        if (UsedCard5.transform.childCount > 0)
        {
            UsedCard5.transform.GetChild(0).transform.SetParent(FreeCards);
        }
        if (UsedCard6.transform.childCount > 0)
        {
            UsedCard6.transform.GetChild(0).transform.SetParent(FreeCards);
        }
        if (UsedCard7.transform.childCount > 0)
        {
            UsedCard7.transform.GetChild(0).transform.SetParent(FreeCards);
        }
    }
}
