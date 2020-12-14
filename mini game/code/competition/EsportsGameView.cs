using UnityEngine;
using UnityEngine.UI;

namespace esports
{
    public class EsportsGameView : MonoBehaviour
    {
        private string tetrisText;
        private string f1Text;
        private string cardText;
        private string ww3Text;
        private string pianoText;
        private string nbaText;
        private string towerText;
        private string punchText;


        private void Start()
        {
            tetrisText = "fa-cube";
            f1Text = "fa-shipping-fast";
            cardText = "fa-chess-board";
            ww3Text = "fa-fighter-jet";
            pianoText = "fa-itunes-note";
            nbaText = "fa-basketball-ball";
            towerText = "fa-chess-rook";
            punchText = "fa-hand-rock";
        }

        public string GetEsportsGameText(EsportsGame game)
        {
            if (game == EsportGameInitConst.ARENA_OF_GLORY)
            {
                return towerText;
            }
            else if (game == EsportGameInitConst.CITY_FIGHTER)
            {
                return punchText;
            }
            else if (game == EsportGameInitConst.F1_LEGENDS)
            {
                return f1Text;
            }
            else if (game == EsportGameInitConst.GREAT_PIANIST)
            {
                return pianoText;
            }
            else if (game == EsportGameInitConst.NBA)
            {
                return nbaText;
            }
            else if (game == EsportGameInitConst.TETRIS)
            {
                return tetrisText;
            }
            else if (game == EsportGameInitConst.THE_CARD_MASTER)
            {
                return cardText;
            }
            else
            {
                return ww3Text;
            }
        }
    }

}