using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace esports
{
    public abstract class TrainingActivity
    {
        public string nameField;
        public string descriptionField;
        public List<Qualification> unlockRequirements;
        
        protected TrainingActivity(string nameField,string descriptionField, List<Qualification> unlockRequirements)
        {
            this.nameField = nameField;
            this.descriptionField = descriptionField;
            this.unlockRequirements = unlockRequirements;
        }
        public abstract void Training(Player player);

        public bool UnlockActivity(Player player)
        {
            if (unlockRequirements == null)
            {
                return true;
            }
            foreach (Qualification quali in unlockRequirements)
            {
                if (!QualificationFactory.FulfillQualification(quali, player))
                {
                    return false;
                }

            }
            return true;
        }
    }

}