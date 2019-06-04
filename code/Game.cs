class Game {
    
    private Player[] players;
    GameState state;
    int day = 0;
    public Game() {
        players = new Player[Constants.NUM_PLAYERS];

        for (int i = 0; i < Constants.NUM_PLAYERS; i++) {
            players[i] = new Spy();
        }

        state = GameState.PASSIVE;
        day = 0;
    }

    public void newDay() {
        day++;
        state = GameState.PASSIVE;
        startExecutePassives();
    }

//TODO
    void startExecutePassives() {
        state = GameState.DECLARE_SEARCH;
        startDeclareSearch();
    }

    void startDeclareSearch() {
        state= GameState.SEARCH;
    }

}