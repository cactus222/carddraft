using System.Collections.Generic;

class Cost{
    int rep = 0;
    int funds = 0;
    List<Skill> reqSkills = new List<Skill>();
    List<Card> reqCards = new List<Card>();
    List<Card> consumedCards = new List<Card>();
    public Cost(int rep, int funds, List<Skill> skills, List<Card> reqCards, List<Card> consumedCards) {
        this.rep = rep;
        this.funds = funds;
        this.reqSkills = new List<Skill>(skills);
        this.reqCards = new List<Card>(reqCards);
        this.consumedCards = new List<Card>(consumedCards);
    }
    public int getRep() { return rep; }
    public int getFunds() { return funds;}
    public List<Skill> getRequiredSkills(){return reqSkills;}
    public List<Card> getRequiredCards(){return reqCards;}
    public List<Card> getConsumedCards(){return consumedCards;}
}