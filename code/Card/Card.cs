using System.Collections.Generic;
abstract class Card {

    virtual public void play() {

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

}