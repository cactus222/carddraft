using System.Collections.Generic;

class Shore: Place {
    //TOOD
    override public List<Card> generateCards(int count, bool includePromo, int round) {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < count; i++) {
            cards.Add(new Fish());
        }
        return cards;
    }
}