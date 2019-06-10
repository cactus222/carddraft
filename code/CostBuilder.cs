using System.Collections.Generic;

public class CostBuilder {
    int rep = 0;
    int funds = 0;
    List<Skill> reqSkills = new List<Skill>();
    List<Card> reqCards = new List<Card>();
    
    List<Card> consumedCards = new List<Card>();
    bool promoteReq = false;
    public CostBuilder setRep(int rep) {
        this.rep = rep;
        return this;
    }

    public CostBuilder setFunds(int funds) {
        this.funds = funds;
        return this;
    }
    public CostBuilder setReqSkills(List<Skill> reqSkills) {
        this.reqSkills = reqSkills;
        return this;
    }
    public CostBuilder setReqCards(List<Card> reqCards) {
        this.reqCards = reqCards;
        return this;
    }

    public CostBuilder setConsumedCards(List<Card> consumedCards) {
        this.consumedCards = consumedCards;
        return this;
    }

    public CostBuilder setNeedsPromotion(bool b) {
        this.promoteReq = b;
        return this;
    }

    public Cost build() {
        return new Cost(rep, funds, reqSkills, reqCards, consumedCards, promoteReq);
    }
}