using System.Collections.Generic;

public class Office: Place {
    //TODO
    
    private int percentageAction = 25;
    
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
    //action
    List<SearchCard> actionCards = new List<SearchCard>(){
        new SearchCard(new TheTalk(), 5 ),
         new SearchCard(new WashroomBreak(), 7),
    };

    List<SearchCard> itemCards = new List<SearchCard>(){
        new SearchCard(new Pen(), 5 ),
         new SearchCard(new Stapler(), 7),
         new SearchCard(new Staples(), 3),
    };

}