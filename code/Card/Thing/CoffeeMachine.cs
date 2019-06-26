using System.Collections.Generic;

public class CoffeeMachine: Card {
    // override public Cost getBaseCost() {
    //     return new CostBuilder().setFunds(1).build(); 
    // }

    override public bool isCraftable() {
        return true;
    }

    override public Cost getCraftCost() {
        return new CostBuilder()
            .setReqSkills(new List<Skill>{Skill.WOOD_WORKING})
            .setReqCards(new List<Card>{new Wood()})
            .setConsumedCards(new List<Card>{new Wood()})
            .build();
    }
}