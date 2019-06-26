using System.Collections.Generic;
public class Battery: Card {
    override public bool isCraftable() {
        return true;
    }

    override public Cost getCraftCost() {
        return new CostBuilder()
            .setReqSkills(new List<Skill>())
            .setReqCards(new List<Card>{new Potato(), new Wires()})
            .setConsumedCards(new List<Card>{new Potato(), new Wires()})
            .build();
    }
}