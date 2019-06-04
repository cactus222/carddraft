using System.Collections.Generic;

class Document: Card {

    override public bool isCraftable() {
        return true;
    }
    override public Cost getCraftCost() {
        return new CostBuilder().setReqCards(new List<Card>(){new Pen(), new Paper()}).build();
    }

}