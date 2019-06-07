using System.Collections.Generic;
abstract class Card {

    virtual public int getCoolDown() {
        return 0;
    }

    virtual public void play(Game g) {

    }

    virtual public Cost getPlayCost() {
        return new CostBuilder().build();
    }

    virtual public bool isConsumedOnPlay() {
        return true;
    }

    virtual public bool isPlayable() {
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

}