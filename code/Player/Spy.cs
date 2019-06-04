using System.Collections.Generic;
class Spy: Player {
    
    private List<Goal> goals;
    public Spy() {
        goals = new List<Goal>(){ new EliminateAllGoal() };
    }
    
    override public List<Goal> getGoals() {
        return goals;
    }
}