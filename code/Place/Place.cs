using System.Collections.Generic;

abstract class Place {
    public abstract List<Card> generateCards(int count, bool includePromo, int round);
}