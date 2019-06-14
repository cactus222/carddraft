using System.Collections.Generic;

public class Workshop: Place {
    //TOOD
    override public List<Card> generateCards(int count, bool includePromo, int round) {
        
        List<Card> cards = new List<Card>();
        for (int i = 0; i < count; i++) {
            if (Utils.randInt(0, 8) > 0) {
                cards.Add(pickRandom(itemCards));
            } else {
                cards.Add(pickRandom(actionCards));
            }
        }
        
        
        return cards;
    }

    List<SearchCard> actionCards = new List<SearchCard>(){
        new SearchCard(new TheTalk(), 5 ),
         new SearchCard(new WashroomBreak(), 7),
    };

    List<SearchCard> itemCards = new List<SearchCard>(){
        new SearchCard(new Wood(), 5 ),
         new SearchCard(new Cardboard(), 7),
         new SearchCard(new WoodWorkingManual(), 3),
    };
}