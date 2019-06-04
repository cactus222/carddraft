class EliminateAllGoal: Goal {
    //TODO
    override public bool isComplete(Game g) {
        return false;
    }
    override public string getName() {
        return "Eliminate";
    }
    override public string getDesc() {
        return "Eliminate all other players";
    }
}