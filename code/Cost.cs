using System.Collections.Generic;

class Cost{
    int rep = 0;
    int money = 0;
    int skillPts = 0;
    int charmPts = 0;
    List<Skill> reqSkills = new List<Skill>();
    List<Card> reqCards = new List<Card>();
    public Cost(int rep, int money, int skillPts, int charmPts, List<Skill> skills, List<Card> reqCards) {
        this.rep = rep;
        this.money = money;
        this.skillPts = skillPts;
        this.charmPts = charmPts;
        this.reqSkills = new List<Skill>(skills);
        this.reqCards = new List<Card>(reqCards);
    }
    public int getRep() { return rep; }
    public int getMoney() { return money;}
    public int getSkillPts() { return skillPts;}
    public int getCharmPts() { return charmPts;}
    public List<Skill> requiredSkills(){return reqSkills;}
    public List<Card> requiredCards(){return reqCards;}
}