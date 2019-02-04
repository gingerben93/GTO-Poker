using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GetServerInfo : MonoBehaviour
{
    const string pathway = "deck_of_pixel_cards_by_poptarts_at_2am_";

    public const string GETFREECARDS = "http://10.0.0.36:5000/get_free_cards";
    public const string SENDCARDSTOSERVER = "http://10.0.0.36:5000/lock_in_cards";

    //prefabs
    //card prefab
    public GameObject FreeCardPrefab;

    //canvas holders
    public GameObject FreeCards;

    //unity buttons
    public Text ResponseText;
    public GameObject ResetCardsButton;
    public GameObject NextHandButton;

    //player rows
    public List<GameObject> PlayerRowHolder;
    public GameObject PlayerTopCards;
    public GameObject PlayerMiddleCards;
    public GameObject PlayerBottomCards;

    //Player text gameobjects
    public List<GameObject> PlayerTextHolder;
    public GameObject PlayerTopText;
    public GameObject PlayerMiddleText;
    public GameObject PlayerBottomText;

    //opponet text game objects
    public List<GameObject> OpponentTextHolder;
    public GameObject OpponentTopText;
    public GameObject OpponentMiddleText;
    public GameObject OpponentBottomText;

    //opponent Rows
    public List<GameObject> OpponentRowHolder;
    public GameObject OpponentTopCards;
    public GameObject OpponentMiddleCards;
    public GameObject OpponentBottomCards;

    //holder for cards objects
    public List<GameObject> OpponentCardsHolder;
    public GameObject OppenentTopCard1;
    public GameObject OppenentMiddleCard1;
    public GameObject OppenentMiddleCards2;
    public GameObject OppenentBottomCards1;
    public GameObject OppenentBottomCards2;
    public GameObject OppenentBottomCards3;
    public GameObject OppenentBottomCards4;

    //global game info
    GameData GameInformation;
    int currentPlayer = 0;

    public enum Suit
    {
        Spades,
        Clubs,
        Hearts,
        Diamonds
    }

    public enum Number
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public enum HandType
    {
        StraightFlush,
        FourKind,
        FullHouse,
        Flush,
        Straight,
        ThreeKind,
        TwoPair,
        Pair,
        HighCard
    }

    [System.Serializable]
    public struct Card
    {
        public Number value;
        public Suit suit;

        public Card(Number val, Suit s)
        {
            value = val;
            suit = s;
        }
    }

    public class CommunityCards
    {
        public List<int> community_card_values { get; set; }
        public List<int> commuity_card_suits { get; set; }
    }

    public class PlayerData
    {
        public string player_name { get; set; }
        public List<int> player_card_values { get; set; }
        public List<int> player_card_suits { get; set; }
        public List<int> top_cards_values { get; set; }
        public List<int> top_cards_suits { get; set; }
        public List<int> middle_cards_values { get; set; }
        public List<int> middle_cards_suits { get; set; }
        public List<int> bottom_cards_values { get; set; }
        public List<int> bottom_cards_suit { get; set; }
        public List<string> hand_types { get; set; }
        public List<string> list_win_lost { get; set; }
    }

    public class GameData
    {
        public CommunityCards communityCards { get; set; }
        public List<PlayerData> playerData { get; set; }
    }


    // Use this for initialization
    private void Start()
    {
        //add player rows to holder
        PlayerRowHolder = new List<GameObject>();
        PlayerRowHolder.Add(PlayerTopCards);
        PlayerRowHolder.Add(PlayerMiddleCards);
        PlayerRowHolder.Add(PlayerBottomCards);

        //add player gameobjects to holders
        PlayerTextHolder = new List<GameObject>();
        PlayerTextHolder.Add(PlayerTopText);
        PlayerTextHolder.Add(PlayerMiddleText);
        PlayerTextHolder.Add(PlayerBottomText);

        //add player rows to holder
        OpponentRowHolder = new List<GameObject>();
        OpponentRowHolder.Add(OpponentTopCards);
        OpponentRowHolder.Add(OpponentMiddleCards);
        OpponentRowHolder.Add(OpponentBottomCards);

        //add opponet gameobjects to holders
        OpponentTextHolder = new List<GameObject>();
        OpponentTextHolder.Add(OpponentTopText);
        OpponentTextHolder.Add(OpponentMiddleText);
        OpponentTextHolder.Add(OpponentBottomText);

        //add cards to holder
        OpponentCardsHolder = new List<GameObject>();
        OpponentCardsHolder.Add(OppenentTopCard1);
        OpponentCardsHolder.Add(OppenentMiddleCard1);
        OpponentCardsHolder.Add(OppenentMiddleCards2);
        OpponentCardsHolder.Add(OppenentBottomCards1);
        OpponentCardsHolder.Add(OppenentBottomCards2);
        OpponentCardsHolder.Add(OppenentBottomCards3);
        OpponentCardsHolder.Add(OppenentBottomCards4);

        //start game
        StartCoroutine(GetFreeCards());
    }

    IEnumerator GetFreeCards()
    {
        //set reset cards button to active
        ResetCardsButton.SetActive(true);

        UnityWebRequest wwwRequest = UnityWebRequest.Get(GETFREECARDS);
        yield return wwwRequest.SendWebRequest();
        if (wwwRequest.isNetworkError || wwwRequest.isHttpError)
        {
            Debug.Log(wwwRequest.error);
        }
        else
        {
            //parse json string for values and suit list
            string[] words = wwwRequest.downloadHandler.text.Split('[',']');
            string valuesList = words[2];
            string suitsList = words[4];
            string[] valuesString = valuesList.Split(',');
            string[] suitsString = suitsList.Split(',');

            List<int> values = new List<int>();
            List<int> suits = new List<int>();
            foreach (var c in valuesString)
            {
                values.Add(Int32.Parse(c));
            }
            foreach (var c in suitsString)
            {
                suits.Add(Int32.Parse(c));
            }

            //after parse make cards
            SpawnCardsInCenter(values, suits, 7);
        }
    }

    //create X number in center row; for player cards then community cards
    void SpawnCardsInCenter(List<int> values, List<int> suits, int NumberCards)
    {
        for (int x = 0; x < NumberCards; x++)
        {
            CreatSingleCard(values[x], suits[x], FreeCards.transform);
        }
    }

    //create prefab for card
    void CreatSingleCard(int value, int suit, Transform LocationToPlace)
    {
        int ImageNumber = value - 2 + suit * 13;
        GameObject cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        GameObject prefab = Instantiate(FreeCardPrefab, LocationToPlace);
        prefab.GetComponent<Image>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.name = (Number)(value - 2) + " of " + (Suit)suit;
    }

    //get card info from row
    List<Card> getCardsFromRows(string rowName)
    {
        List<Card> temp = new List<Card>();

        foreach (Transform child in GameObject.Find(rowName).transform)
        {
            Number cardNumber = Number.Ace;
            Suit cardSuit = Suit.Spades;
            string PlayerCard = child.GetChild(0).name;
            string[] ssizes = PlayerCard.Split(' ');
            foreach (string s in ssizes)
            {
                try
                {
                    cardNumber = (Number)System.Enum.Parse(typeof(Number), s);
                }
                catch
                {

                }
                try
                {
                    cardSuit = (Suit)System.Enum.Parse(typeof(Suit), s);
                }
                catch
                {

                }
            }
            temp.Add(new Card(cardNumber, cardSuit));
        }
        return temp;
    }

    public void SubmitCards()
    {
        //disable buttons
        PlayerController.PlayerControllerSingle.SubmitButton.SetActive(false);
        PlayerController.PlayerControllerSingle.ResetButton.SetActive(false);

        StartCoroutine(SendCardsToServer());
    }

    IEnumerator SendCardsToServer()
    {
        //game variables
        string value1;
        string suit1;
        string value2;
        string suit2;
        string value3;
        string suit3;
        string value4;
        string suit4;
        string value5;
        string suit5;
        string value6;
        string suit6;
        string value7;
        string suit7;

        List<Card> top = new List<Card>();
        List<Card> middle = new List<Card>();
        List<Card> bottom = new List<Card>();

        top = getCardsFromRows("Top");
        middle = getCardsFromRows("Middle");
        bottom = getCardsFromRows("Bottom");

        //temp set of web requests
        value1 = ((int)top[0].value + 2).ToString();
        suit1 = ((int)top[0].suit).ToString();
        value2 = ((int)middle[0].value + 2).ToString();
        suit2 = ((int)middle[0].suit).ToString();
        value3 = ((int)middle[1].value + 2).ToString();
        suit3 = ((int)middle[1].suit).ToString();
        value4 = ((int)bottom[0].value + 2).ToString();
        suit4 = ((int)bottom[0].suit).ToString();
        value5 = ((int)bottom[1].value + 2).ToString();
        suit5 = ((int)bottom[1].suit).ToString();
        value6 = ((int)bottom[2].value + 2).ToString();
        suit6 = ((int)bottom[2].suit).ToString();
        value7 = ((int)bottom[3].value + 2).ToString();
        suit7 = ((int)bottom[3].suit).ToString();

        //must be of this form
        string jsonString = "{\"top\": {\"card1\": {\"value\": \"" + value1 + "\" ,\"suit\": \"" + suit1 + "\"}}," +
                            "\"middle\": {\"card1\": {\"value\": \"" + value2 + "\" ,\"suit\": \"" + suit2 + "\"},\"card2\": {\"value\": \"" + value3 + "\" ,\"suit\": \"" + suit3 + "\"}}," +
                            "\"Bottom\": {\"card1\": {\"value\": \"" + value4 + "\" ,\"suit\": \"" + suit4 + "\"},\"card2\": {\"value\": \"" + value5 + "\" ,\"suit\": \"" + suit5 + "\"},\"card3\": {\"value\": \"" + value6 + "\" ,\"suit\": \"" + suit6 + "\"},\"card4\": {\"value\": \"" + value7 + "\" ,\"suit\": \"" + suit7 + "\"}}}";

        var request = new UnityWebRequest(SENDCARDSTOSERVER, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        print(request.downloadHandler.text);

        //deserialize json to c# class
        GameInformation = JsonConvert.DeserializeObject<GameData>(request.downloadHandler.text);

        //set community cards
        SpawnCardsInCenter(GameInformation.communityCards.community_card_values, GameInformation.communityCards.commuity_card_suits, 5);

        //change color for win or lose
        for(int i = 0; i < PlayerRowHolder.Count; i++)
        {
            if (GameInformation.playerData[0].list_win_lost[i] == "win")
            {
                PlayerRowHolder[i].GetComponent<Image>().color = Color.green;
            }
            else
            {
                PlayerRowHolder[i].GetComponent<Image>().color = Color.red;
            }
        }

        //set text
        for (int i = 0; i < PlayerTextHolder.Count; i++)
        {
            PlayerTextHolder[i].GetComponent<Text>().text = GameInformation.playerData[0].hand_types[i];
        }

        //set opponent
        SetOpponentInformation(GameInformation.playerData[1]);

        ////turn on next hand button
        NextHandButton.SetActive(true);
    }

    void SetOpponentInformation(PlayerData playerInfo)
    {
        //set text
        for (int i = 0; i < OpponentTextHolder.Count; i++)
        {
            OpponentTextHolder[i].GetComponent<Text>().text = playerInfo.hand_types[i];
        }

        //change color
        for (int i = 0; i < OpponentRowHolder.Count; i++)
        {
            if (playerInfo.list_win_lost[i] == "win")
            {
                OpponentRowHolder[i].GetComponent<Image>().color = Color.green;
            }
            else
            {
                OpponentRowHolder[i].GetComponent<Image>().color = Color.red;
            }
        }

        //place cards
        for (int x = 0; x < OpponentCardsHolder.Count; x++)
        {
            CreatSingleCard(playerInfo.player_card_values[x], playerInfo.player_card_suits[x], OpponentCardsHolder[x].transform);
        }
    }

    public void CycleLeftPlayer()
    {
        currentPlayer--;
        if(currentPlayer == 0)
        {
            currentPlayer = GameInformation.playerData.Count - 1;
        }
        SetOpponentInformation(GameInformation.playerData[currentPlayer]);
    }

    public void CycleRightPlayer()
    {
        currentPlayer++;
        if (currentPlayer == GameInformation.playerData.Count - 1)
        {
            currentPlayer = 1;
        }
        SetOpponentInformation(GameInformation.playerData[currentPlayer]);
    }
}
