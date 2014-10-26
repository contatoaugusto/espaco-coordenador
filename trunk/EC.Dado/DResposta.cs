using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Dado
{
    public class DResposta
    {

        public List<RESPOSTA> ConsultarResposta()
        {
            using (ECEntities db = new ECEntities())
            {
                var r = db.RESPOSTA.ToList();
                List<RESPOSTA> ltResposta = new List<RESPOSTA>();

                foreach (var tipo in r)
                {
                    RESPOSTA resposta = new RESPOSTA();
                    resposta.ID_QUESTAO = tipo.ID_RESPOSTA;
                    resposta.TEXTO = tipo.TEXTO;
                    ltResposta.Add(resposta);
                }

                return ltResposta;
            }
        }

        public List<RESPOSTA> ConsultarRespostaByQuestao(int idQuestao)
        {
            using (ECEntities db = new ECEntities())
            {
                var r = db.RESPOSTA.Where(rs => rs.ID_QUESTAO == idQuestao);
                
                List<RESPOSTA> ltResposta = new List<RESPOSTA>();

                foreach (var tipo in r)
                {
                    RESPOSTA resposta = new RESPOSTA();
                    resposta.ID_RESPOSTA = tipo.ID_RESPOSTA;
                    resposta.ID_QUESTAO = tipo.ID_QUESTAO;
                    resposta.TEXTO = tipo.TEXTO;
                    resposta.RESPOSTA_CORRETA = tipo.RESPOSTA_CORRETA;
                    ltResposta.Add(resposta);
                }

                return ltResposta;
            }
        }
    }
}
