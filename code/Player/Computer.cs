using System.Collections.Generic;
 class Computer: Player {
    
    public Computer(Role r) :base(r) {
        
    }
    override public void visit(Game g) {
        g.receiveVisit(this, new Forest());
    }

    override public void purchase(Game g) {
        List<Card> cards = g.getPurchasableCards(this);
        int numCards = g.getMaxPurchasableCardsCount(this);
        
        List<Card> purchase = cards.GetRange(0, numCards);
        g.receivePurchase(this, purchase);
    }

    override  public void sell(Game g) {
        g.receiveSell(this, new List<Card>());
    }

    override public void craft(Game g) {
        g.receiveCraftFinish(this);
    }

    override public void action(Game g) {
        g.receiveActionFinished(this);    
    }
    
}