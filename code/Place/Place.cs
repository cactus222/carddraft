using System.Collections.Generic;

abstract public class Place {
    public abstract List<Card> generateCards(int count, bool includePromo, int round);

    private Card pickSelected(List<SearchCard> cardList, int selected) {
        int sum = 0;
        foreach(SearchCard cardRate in cardList) {
            sum += cardRate.rate;
            if (selected < sum) {
                return cardRate.card;
            }
        }
        // the fk?
        throw new System.Exception("i messed up somewhere");
    }

    protected int getTotal(List<SearchCard> d) {
        int sum = 0;
        foreach(SearchCard kvp in d) {
            sum += kvp.rate;
        }
        return sum;
    }
    protected Card pickRandom(List<SearchCard> cards) {
        int total = getTotal(cards);
        int selected = Utils.randInt(0, total);
        return pickSelected(cards, selected);
    }

}