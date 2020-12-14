using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace esports {
    public class TrainingFactory
    {
        public static TrainingActivity GetTraining(EsportsGame game, string nameField, string descriptionField, List<Qualification> unlockRequirements)
        {
            return new PlayGameTraining(game, nameField,descriptionField, unlockRequirements);
        }
    }

}