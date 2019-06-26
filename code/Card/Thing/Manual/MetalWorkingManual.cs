using System.Collections.Generic;

public class MetalWorkingManual: Card {
    override public bool isPlayable(Game g, Player owner) {
        return owner.getLearningSkill() == Skill.NONE && !owner.getSkills().Contains(Skill.METAL_WORKING);
    }

    override public void play(Game g, Player owner) {
        owner.setLearningSkill(Skill.METAL_WORKING, 2);
    }

}