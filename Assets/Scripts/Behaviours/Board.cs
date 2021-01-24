using UnityEngine;

public class Board : SingletonBehaviour<Board>
{
    public HandBehaviour userHand;
    public HandBehaviour opponentHand;

    public CardDestinationBehaviour userWins;
    public CardDestinationBehaviour opponentWins;

    public CardDestinationBehaviour userDraw;
    public CardDestinationBehaviour opponentDraw;

    public DeckBehaviour deck;
    public CardDestinationBehaviour sacrifice;
}
