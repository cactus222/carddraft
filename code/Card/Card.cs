using System.Collections.Generic;
public abstract class Card {

    virtual public int getCoolDown() {
        return 0;
    }

    virtual public void play(Game g, Player owner) {

    }

    virtual public Cost getPlayCost() {
        return new CostBuilder().build();
    }

    virtual public bool isConsumedOnPlay() {
        return true;
    }

    virtual public bool isPlayable(Game g, Player owner) {
        return false;
    }

    virtual public bool isCraftable() {
        return false;
    }

    virtual public Cost getCraftCost() {
        return new CostBuilder().build();
    }

    virtual public int getFundCost() {
        return 0;
    }
    virtual public bool isSellable() {
        return true;
    }
    virtual public int getFundSell() {
        return 0;
    }

    public override string ToString() {
        return this.GetType().Name;
    }

}