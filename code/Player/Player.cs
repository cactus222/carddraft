using System.Collections.Generic;
abstract class Player {
    protected int morale;
    protected int reputation;
    protected int funds;

    List<Card> board;
    List<Card> hand;

    List<Skill> skills;
    Role role;
    protected string name;

    bool promoted;

    protected Player(Role role) {
        morale = 10;
        board = new List<Card>();
        hand = new List<Card>();
        skills = new List<Skill>();
        reputation = 50;
        funds = 100;
        this.role = role;
        promoted = false;
    }

    public bool isPromoted() {
        return promoted;
    }
    
    public List<Goal> getGoals() {
        return role.getGoals();
    }

    public List<Card> getBoard() {
        return board;
    }
    public List<Card> getHand() {
        return hand;
    }

    abstract public void visit(Game g);
    abstract public void purchase(Game g);
    abstract public void sell(Game g);
    abstract public void craft(Game g);
    abstract public void action(Game g);
    
    public int getFunds() {
        return funds;
    }

    public void addFunds(int f) {
        funds += f;
    }

    public void addCardsToHand(List<Card> cards) {
        hand.AddRange(cards);
    }

    public List<Card> getAllCards() {
        List<Card> allCards = new List<Card>(hand);
        allCards.AddRange(board);
        return allCards;
    }
    
    public int getRep() {
        return reputation;
    }
    public List<Skill> getSkills() {
        return skills;
    }

    public void removeCards(List<Card> cards) {
        //TODO
    }

    public void addRep(int rep) {
        this.reputation += rep;
    }

}