using System.Collections.Generic;
abstract class Player {
    protected int reputation;
    protected int money;
    protected int skillPts;
    protected int charmPts;

    List<Card> board;
    List<Card> hand;

    protected Player() {
        board = new List<Card>();
        hand = new List<Card>();
        reputation = 50;
        money = 100;
        skillPts = 50;
        charmPts = 50;
    }
    
    public abstract List<Goal> getGoals();

    public List<Card> getBoard() {
        return board;
    }
    public List<Card> getHand() {
        return hand;
    }
    
}