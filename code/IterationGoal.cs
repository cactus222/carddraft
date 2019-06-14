public class IterationGoal {
    Card c;
    int count;

    int completionRep;
    int completionFunds;

    public IterationGoal(Card c, int count, int completionRep, int completionFunds) {
        this.c = c;
        this.count = count;
        this.completionRep = completionRep;
        this.completionFunds = completionFunds;
    }
    public int getCount() {
        return count;
    }

    public Card getCard() {
        return c;
    }
    public int getCompletionRep() {
        return completionRep;
    }
    public int getCompletionFunds() {
        return completionFunds;
    }
}