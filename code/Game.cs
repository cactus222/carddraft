using System.Collections.Generic;
public class Game {

    private List<Player> players;
    private List<Player> lostPlayers;

    GameState state;

    int failedDeadlines;
    int round;
    int day = 0;
    int priority = 0;
    Company company;

    Place[] visitPlaces;
    bool[] hasActed;
    List<List<Card>> purchaseableCards;
    private int purchaseRound;

    public Game() {
        players = new List<Player>();
        lostPlayers = new List<Player>();
        players.Add(new Human(new Spy()));
        for (int i = 1; i < Constants.NUM_PLAYERS; i++) {
            // for (int i = 0; i < Constants.NUM_PLAYERS; i++) {
            players.Add(new Computer(new Spy()));
        }
        company = new Company();
        state = GameState.PASSIVE;
        day = 0;
        round = 0;
        failedDeadlines = 0;
        priority = 0;
    }

    // Game interface
    public void newDay() {
        day++;
        startPassivesPhase();
    }


    public void receiveVisit(Player player, Place place) {
        Logger.logMessage($"Received visit from {getPlayerIndex(player)}");
        if (state != GameState.VISIT) {
            Logger.logMessage("Cannot declare visit while not in phase");
            return;
        }
        int index = getPlayerIndex(player);
        if (index == -1) {
            Logger.logMessage("wdf this player doesnt exist");
            return;
        }

        visitPlaces[index] = place;
        hasActed[index] = true;
        if (hasAllActed()) {
            endVisitPhase();
        }
    }

    public bool receivePurchase(Player p, List<Card> cards) {
        Logger.logMessage($"Received purchase from {getPlayerIndex(p)} {Utils.getCardListString(cards)}");
        if (state != GameState.PURCHASE) {
            Logger.logMessage("Cannot declare purchase while not in phase");
            return false;
        }
        //TODO check max hand size
        int index = getPlayerIndex(p);
        if (hasActed[index]) {
            Logger.logMessage("already bought");
            return false;
        }
        if (cards.Count + p.getHand().Count > Constants.MAX_HAND_SIZE) {
            Logger.logMessage("cant be bigger than amx hand size");
            return false;
        }
        List<Card> purchasable = new List<Card>(getPurchasableCards(p));
        int cost = 0;
        foreach (Card c in cards) {
            int cIndex = purchasable.IndexOf(c);
            if (cIndex == -1) {
                Logger.logMessage("wdf this card wasnt buyable");
                return false;
            } else {
                purchasable.RemoveAt(cIndex);
            }
            cost += c.getFundCost();
        }

        if (p.getFunds() > cost) {
            p.addFunds(-cost);
            p.addCardsToHand(cards);
            List<Card> kitty = getPurchasableCards(p);
            Logger.logMessage($"before {Utils.getListString(kitty)}");
            foreach (Card c in cards) {
                kitty.Remove(c);
            }
            Logger.logMessage($"after {Utils.getListString(kitty)}");
            hasActed[index] = true;
            if (hasAllActed()) {
                endPurchaseRound();
            }
            return true;
        } else {
            Logger.logMessage("failed purchase cant afford");
            return false;
        }
    }
    public void receiveSell(Player p, List<Card> cards) {
        Logger.logMessage($"Received sell from {getPlayerIndex(p)} {Utils.getCardListString(cards)}");
        if (state != GameState.SELL) {
            Logger.logMessage("Cannot declare sell while not in phase");
            return;
        }
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

        int index = getPlayerIndex(p);
        purchaseableCards[index].AddRange(cards);
        p.addFunds(value);
        hasActed[index] = true;
        if (hasAllActed()) {
            startPurchasePhase();
        }
    }

    //TODO
    public void contributeToGoal(Player p) {

    }

    public List<Card> getPurchasableCards(Player p) {
        return purchaseableCards[getPlayerIndex(p)];
    }

    //TODO
    public int getMaxPurchasableCardsCount(Player p) {
        return Constants.BASE_PURCHASE_COUNT;
    }

    public void receiveCraft(Player p, Card c) {
        if (state != GameState.CRAFT) {
            Logger.logMessage("Cannot declare craft while not in phase");
            return;
        }
        //TODO
        if (canPlayerCraft(p, c)) {
            pay(p, c.getCraftCost());
            p.addCardsToHand(new List<Card>() { c });
        } else {
            Logger.logMessage("failed to craft");
        }
    }

    private void pay(Player p, Cost c) {
        p.addFunds(-c.getFunds());
        p.addRep(-c.getRep());
        p.removeCards(c.getConsumedCards());
    }

    private bool canPlayerCraft(Player p, Card c) {
        if (!c.isCraftable()) {
            return false;
        }
        Cost cost = c.getCraftCost();
        return canPay(p, cost);
    }

    private bool canPay(Player p, Cost c) {
        if (!Utils.hasAllCards(p.getAllCards(), c.getRequiredCards())) {
            return false;
        }
        if (p.getFunds() < c.getFunds()) {
            return false;
        }
        if (p.getRep() < c.getRep()) {
            return false;
        }
        if (c.requiresPromotion() && p.isPromoted()) {
            return false;
        }
        if (!Utils.hasAllSkills(p.getSkills(), c.getRequiredSkills())) {
            return false;
        }

        return true;
    }

    private int getPlayerIndex(Player p) {
        return players.IndexOf(p);
    }
    public void receiveCraftFinish(Player p) {
        if (state != GameState.CRAFT) {
            Logger.logMessage("Cannot declare craft finished while not in phase");
            return;
        }
        Logger.logMessage($"Received craft finish from {getPlayerIndex(p)}");
        int index = getPlayerIndex(p);
        if (index == -1) {
            Logger.logMessage("wat cant receive craft finish from unknown player");
            return;
        }
        hasActed[index] = true;
        if (hasAllActed()) {
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

    public void receiveAction(Player p, Card c) {
        if (state != GameState.ACTION) {
            Logger.logMessage("Cannot declare action while not in phase");
            return;
        }
        Logger.logMessage($"Received action from {getPlayerIndex(p)} {c}");
        //Playing card? or activating from board

        if (!c.isPlayable(this, p)) {
            Logger.logMessage($"Card is not playable {c}");
            return;
        }

        if (!canPay(p, c.getPlayCost())) {
            Logger.logMessage($"Can't afford to pay for card {c}");
            return;
        }

        p.playCard(this, c);
    }

    public void receiveActionFinished(Player p) {
        if (state != GameState.ACTION) {
            Logger.logMessage("Cannot declare action finished while not in phase");
            return;
        }
        Logger.logMessage($"Received action finish from {getPlayerIndex(p)}");
        int index = getPlayerIndex(p);
        hasActed[index] = true;
        requestPlayerAction();
    }
    //TODO
    public Player getCurrentPlayer() {
        return players[0];
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
        hasActed = new bool[players.Count];
        for (int i = 0; i < players.Count; i++) {
            visitPlaces[i] = new Office();
        }
        foreach (Player player in players) {
            player.visit(this);
        }
        logGameState();
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
        hasActed = new bool[players.Count];
        for (int i = 0; i < players.Count; i++) {
            Player player = players[i];
            player.sell(this);
        }
        logGameState();
    }


    /*
    * Purchase
     */

    private void startPurchasePhase() {
        state = GameState.PURCHASE;
        hasActed = new bool[players.Count];
        purchaseRound = 0;
        for (int i = 0; i < players.Count; i++) {
            Player player = players[i];
            // List<Card> cards = purchaseableCards[i];
            player.purchase(this);
        }
        logGameState();
    }

    private void endPurchaseRound() {
        purchaseRound++;
        Logger.logMessage("end purchase round " + purchaseRound  + " of " + (Constants.NUM_PLAYERS + 1) );
        if (purchaseRound == 1 + Constants.NUM_PLAYERS) {
            startCraftPhase();
            return;
        }

        //Pass cards around to next? or maybe random
        int numPeople = players.Count;
        hasActed = new bool[numPeople];
        List<Card> cards = purchaseableCards[0];
        for (int i = 0; i < numPeople; i++) {
            List<Card> nextCards = purchaseableCards[(i + numPeople + 1) % numPeople];
            purchaseableCards[(i + numPeople + 1) % numPeople] = cards;
            cards = nextCards;
        }
        // get computers to act
        for (int i = 0; i < players.Count; i++) {
            Player player = players[i];
            // List<Card> cards = purchaseableCards[i];
            player.purchase(this);
        }

        logGameState();
    }

    /*
    ** Craft Phase
     */
    private void startCraftPhase() {
        state = GameState.CRAFT;
        hasActed = new bool[players.Count];
        foreach (Player p in players) {
            p.craft(this);
        }
        logGameState();
    }
    /*
    ** action Phase
     */
    private void startActionPhase() {
        state = GameState.ACTION;
        hasActed = new bool[players.Count];
        requestPlayerAction();
        logGameState();
    }

    private void requestPlayerAction() {
        if (hasAllActed()) {
            startEndRoundPhase();
            return;
        }
        //TODO activate stuff/pass
        priority = (priority + 1) % players.Count;
        Player player = players[priority];
        player.action(this);

    }


    /*
    ** end round Phase
     */

    private void startEndRoundPhase() {
        state = GameState.END_ROUND;
        //TODO salary etc.?
        //round bump

        foreach (Player p in players) {
            p.addFunds(1);
            p.dayRotate();
        }
        day++;
        if (day % 14 == 0) {
            round++;
            day = 0;
        }
        //TODO round/day etc. rotation + flip
        startPassivesPhase();
    }

    private void logPhase() {
        Logger.logMessage(state.ToString("G"));
    }
    private void logGameState() {
        if (state == GameState.SELL) {
            Logger.logMessage("sell");
        } else if (state == GameState.PURCHASE) {
            Logger.logMessage("purchase");
            Logger.logMessage($"buy {Utils.getCardListString(getPurchasableCards(getCurrentPlayer()))}");
        } else if (state == GameState.CRAFT) {
            Logger.logMessage("craft");
        } else if (state == GameState.ACTION) {
            Logger.logMessage("action");
        }

        logPhase();
        Logger.logMessage($"hand: {Utils.getCardListString(getCurrentPlayer().getHand())}");
        Logger.logMessage($"board: {Utils.getCardListString(getCurrentPlayer().getBoard())}");
        Logger.logMessage($"skills: {Utils.getListString(getCurrentPlayer().getSkills())}");
        Logger.logMessage($"learning: {getCurrentPlayer().getLearningSkill()} , time left: {getCurrentPlayer().getLearningTimeLeft()}");

    }

}