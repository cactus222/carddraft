using System.Collections.Generic;

public class Umbrella: Card {
    // override public Cost getBaseCost() {
    //     return new CostBuilder().setFunds(1).build(); 
    // }
    override public bool isCraftable() {
        return true;
    }

    override public Cost getCraftCost() {
        return new CostBuilder()
            .setReqSkills(new List<Skill>{new ClothCrafting()})
            .setReqCards(new List<Card>{new WoodenStick(), new MetalStick(), new Clothh()})
            .setConsumedCards(new List<Card>{new WoodenStick(), new MetalStick(), new Clothh()})
            .build();
    }
}