using System.Collections.Generic;

public class ClothCrafting: Card {
    override public bool isPlayable(Game g, Player owner) {
        return owner.getLearningSkill() == Skill.NONE && !owner.getSkills().Contains(Skill.CLOTH_CRAFTING);
    }

    override public void play(Game g, Player owner) {
        owner.setLearningSkill(Skill.CLOTH_CRAFTING, 1);
    }

}