using System.Collections.Generic;

public class SpeechBook: Card {
    override public bool isPlayable(Game g, Player owner) {
        return owner.getLearningSkill() == Skill.NONE && !owner.getSkills().Contains(Skill.COMMUNICATION);
    }

    override public void play(Game g, Player owner) {
        owner.setLearningSkill(Skill.COMMUNICATION, 1);
    }

}