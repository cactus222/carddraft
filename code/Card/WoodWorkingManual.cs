using System.Collections.Generic;

public class WoodWorkingManual: Card {
    override public bool isPlayable(Game g, Player owner) {
        return owner.getLearningSkill() == Skill.NONE && !owner.getSkills().Contains(Skill.WOOD_WORKING);
    }

    override public void play(Game g, Player owner) {
        owner.setLearningSkill(Skill.WOOD_WORKING, 1);
    }

}