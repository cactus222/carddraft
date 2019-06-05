using System.Collections.Generic;

class Utils {

    public static bool hasAllCards(List<Card> container, List<Card> contained) {
   
        List<Card> allCards = new List<Card>(container);
     
        foreach(Card c in contained) {
            if (allCards.Contains(c)) {
                allCards.Remove(c);
            } else {
                return false;
            }
        }
        return true;
    
    }
}