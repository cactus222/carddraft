using System.Collections.Generic;

class Utils {
    public static string getCardListString(List<Card> cards) {
        string str = "";
        foreach (Card c in cards) {
            str += c.ToString() + " ";
        }
        return str;
    }
    //TODO just convert to generic
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

    public static bool hasAllSkills(List<Skill> container, List<Skill> contained) {
   
        List<Skill> allSkills = new List<Skill>(container);
     
        foreach(Skill c in contained) {
            if (allSkills.Contains(c)) {
                allSkills.Remove(c);
            } else {
                return false;
            }
        }
        return true;
    
    }
}