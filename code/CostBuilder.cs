using System.Collections.Generic;

class CostBuilder {
    int rep = 0;
    int money = 0;
    int skillPts = 0;
    int charmPts = 0;
    List<Skill> reqSkills = new List<Skill>();
    List<Card> reqCards = new List<Card>();
    public CostBuilder setRep(int rep) {
        this.rep = rep;
        return this;
    }

    public CostBuilder setMoney(int money) {
        this.money = money;
        return this;
    }

    public CostBuilder setSkill(int skillPts) {
        this.skillPts = skillPts;
        return this;
    }

    public CostBuilder setCharm(int charmPts) {
        this.charmPts = charmPts;
        return this;
    }

    public CostBuilder setReqSkills(List<Skill> skills) {
        this.reqSkills = skills;
        return this;
    }

    public CostBuilder setReqCards(List<Card> cards) {
        this.reqCards = reqCards;
        return this;
    }

    public Cost build() {
        return new Cost(rep, money, skillPts, charmPts, reqSkills, reqCards);
    }
}