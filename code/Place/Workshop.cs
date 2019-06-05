using System.Collections.Generic;

class Workshop: Place {
    //TOOD
    override public List<Card> generateCards(int count, bool includePromo, int round) {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < count; i++) {
            cards.Add(new Metal());
        }
        return cards;
    }
}