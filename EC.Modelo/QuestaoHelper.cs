using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;


namespace EC.Modelo
{
    public class QuestaoHelper
    {

        private static QUESTAO QUEST;
        private Image imagem;

        public Image Imagem
        {
            get { return imagem; }
            set { imagem = value; }
        }
        public QUESTAO QUESTAO
        {
            get { return QUEST; }
            set { QUEST = value; }
        }

        

    }
}
