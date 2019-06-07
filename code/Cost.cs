using System.Collections.Generic;

class Cost{
    int rep = 0;
    int funds = 0;
    List<Skill> reqSkills = new List<Skill>();
    List<Card> reqCards = new List<Card>();
    List<Card> consumedCards = new List<Card>();

    bool needsPromote = false;
    public Cost(int rep, int funds, List<Skill> skills, List<Card> reqCards, List<Card> consumedCards, bool needsPromote) {
        this.rep = rep;
        this.funds = funds;
        this.reqSkills = new List<Skill>(skills);
        this.reqCards = new List<Card>(reqCards);
        this.consumedCards = new List<Card>(consumedCards);
        this.needsPromote = needsPromote;
    }
    public int getRep() { return rep; }
    public int getFunds() { return funds;}
    public List<Skill> getRequiredSkills(){return reqSkills;}
    public List<Card> getRequiredCards(){return reqCards;}
    public List<Card> getConsumedCards(){return consumedCards;}

    public bool requiresPromotion(){return needsPromote;}
}