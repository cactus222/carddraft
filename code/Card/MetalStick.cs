using System.Collections.Generic;
class MetalStick: Card {
    override public bool isCraftable() {
        return true;
    }

    override public Cost getCraftCost() {
        return new CostBuilder()
            .setReqSkills(new List<Skill>{new MetalWorking()})
            .setReqCards(new List<Card>{new Metal()})
            .setConsumedCards(new List<Card>{new Metal()})
            .build();
    }
}