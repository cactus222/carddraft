using System.Collections.Generic;
class Game {
    
    private List<Player> players;
    private List<Player> lostPlayers;

    GameState state;

    int failedDeadlines;
    int round;
    int day = 0;
    Company company;

    Place[] visitPlaces;
    bool[] hasBought;
    bool[] hasSold;
    bool[] hasActed;
    List<List<Card>> purchaseableCards;
    private int purchaseRound;

    public Game() {
        players = new List<Player>();
        lostPlayers = new List<Player>();

        for (int i = 0; i < Constants.NUM_PLAYERS; i++) {
            players[i] = new Computer(new Spy());
        }
        company = new Company();
        state = GameState.PASSIVE;
        day = 0;
        round = 0;
        failedDeadlines = 0;
    }

    // Game interface
    public void newDay() {
        day++;
        startPassivesPhase();
    }


    public void receiveVisit(Player player, Place place) {
        if (state != GameState.VISIT) {
            Logger.logMessage("Cannot declare visit while not in phase");
            return;
        }
        int index = players.IndexOf(player);
        if (index == -1) {
            Logger.logMessage("wdf this player doesnt exist");
            return;
        }

        visitPlaces[index] = place;
        if (isVisitFinished()) {
            endVisitPhase();
        }
    }

    public void receivePurchase(Player p, List<Card> cards) {
        int index = players.IndexOf(p);
        if (hasBought[index]) {
            Logger.logMessage("already bought");
            return;
        }
        List<Card> purchasable = new List<Card>(getPurchasableCards(p));
        int cost = 0;
        foreach (Card c in cards) {
            int cIndex = purchasable.IndexOf(c);
            if (cIndex == -1) {
                Logger.logMessage("wdf this card wasnt buyable");
                return;
            } else {
                purchasable.RemoveAt(cIndex);
            }
            cost += c.getFundCost();
        }

        if (p.getFunds() > cost) {
            p.addFunds(-cost);
            p.addCardsToHand(cards);
            hasBought[index] = true;
        } else {
            Logger.logMessage("failed purchase cant afford");
        }
    }
    public void receiveSell(Player p, List<Card> cards) {
        int value = 0;
        if (Utils.hasAllCards(p.getAllCards(), cards)) {
            foreach (Card c in cards) {
                if (!c.isSellable()) {
                    Logger.logMessage("this card is not sellable {c}");
                    return;
                }
                value += c.getFundSell();
            }
        } else {
            Logger.logMessage("cantsell cards you dont own");
        }

        int index = players.IndexOf(p);
        purchaseableCards[index].AddRange(cards);
        p.addFunds(value);
        hasSold[index] = true;
        if (isSellingFinished()) {
            startPurchasePhase();
        }
    }

    private bool isSellingFinished() {
        foreach (bool b in hasSold) {
            if (!b) {
                return false;
            }
        }
        return true;
    }

    public List<Card> getPurchasableCards(Player p) {
        return purchaseableCards[players.IndexOf(p)];
    }

    public int getMaxPurchasableCardsCount(Player p) {
        return Constants.BASE_PURCHASE_COUNT;
    }

    public void receiveCraft(Player p, Card c) {
        //TODO
    }

    public void receiveCraftFinish(Player p) {
        int index = players.IndexOf(p);
        if (index == -1) {
            Logger.logMessage("wat cant receive craft finish from unknown player");
            return;
        }
        hasActed[index] = true;
        if(hasAllActed()) {
            startActionPhase();
        }
    }

    private bool hasAllActed() {
        foreach (bool b in hasActed) {
            if (!b) {
                return false;
            }
        }
        return true;
    }


    //Internals
    /*
        Passives 
    */
    private void startPassivesPhase() {
        //TODO
        state = GameState.PASSIVE;


        startVisitPhase();
    }

    /*
        Declare visits 
    */
    private void startVisitPhase() {
        state = GameState.VISIT;
        visitPlaces = new Place[players.Count];
        for (int i = 0; i < players.Count; i++) {
            visitPlaces[i] = new Office();
        }
        foreach (Player player in players) {
            player.visit(this);
        }
    }

    private bool isVisitFinished() {
        foreach (Place p in visitPlaces) {
            if (p == null) {
                return false;
            }
        }
        return true;
    }
    private void endVisitPhase() {
        purchaseableCards = new List<List<Card>>();

        for (int i = 0; i < players.Count; i++) {
            Place visitedPlace = visitPlaces[i];
            Player visitedPlayer = players[i];
            List<Card> cards = visitedPlace.generateCards(Constants.BASE_VISIT_CARD_COUNT, visitedPlayer.isPromoted(), round);
            purchaseableCards.Add(cards);
        }
        startSellPhase();
    }

    /*
    sell
     */

     private void startSellPhase() {
        state = GameState.SELL;
        hasSold = new bool[players.Count];
         for (int i = 0; i < players.Count; i++) {
            Player player = players[i];
            player.sell(this);
        }
     }

    
    /*
    * Purchase
     */

    private void startPurchasePhase() {
        state = GameState.PURCHASE;
        hasBought = new bool[players.Count];
        purchaseRound = 0;
        for (int i = 0; i < players.Count; i++) {
            Player player = players[i];
            List<Card> cards = purchaseableCards[i];
            player.purchase(this);
        }
    }

    private bool isPurchaseFinished() {
        foreach (bool p in hasBought) {
            if (!p) {
                return false;
            }
        }
        return true;
    }

    private void endPurchaseRound() {
        purchaseRound++;
        if (purchaseRound == 1 + Constants.NUM_PLAYERS) {
            endPurchase();
            return;
        }

        //Pass cards around to next? or maybe random
        int numPeople = players.Count;
        hasBought = new bool[numPeople];
        List<Card> cards = purchaseableCards[0];
        for (int i = 0; i < numPeople; i++) {
            List<Card> nextCards = purchaseableCards[(i + numPeople) % numPeople];
            purchaseableCards[(i + numPeople) % numPeople] = cards;
            cards = nextCards;
        }
    }

    private void endPurchase() {
        startCraftPhase();
    }
    /*
    ** Craft Phase
     */
    private void startCraftPhase() {
        state = GameState.CRAFT;
        hasActed = new bool[players.Count];
        foreach(Player p in players) {
            p.craft(this);
        }
    }
    /*
    ** action Phase
     */
    private void startActionPhase() {
        state = GameState.ACTION;
        //TODO activate stuff/pass
    }
    /*
    ** end round Phase
     */

    private void startEndRoundPhase() {
        state = GameState.END_ROUND;
        //TODO salary etc.?
    }
}