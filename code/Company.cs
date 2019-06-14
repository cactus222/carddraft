public class Company {
    int funds;
    // Card goalCard;
    // int goalItem;
    IterationGoal goal;
    int contributedCount = 0;

    //TODO
    public void nextIterationGoal(int round) {
        contributedCount = 0;
        goal = new IterationGoal(new Umbrella(), 5, 10, 10);
    }

    public IterationGoal getIterationGoal() {
        return goal;
    }

    public bool contribute(Card c) {
        if (c.Equals(goal.getCard()) && contributedCount < goal.getCount()) {
            contributedCount++;
            return true;
        }
        return false;
    }

    public bool isGoalComplete() {
        return contributedCount == goal.getCount();
    }
}