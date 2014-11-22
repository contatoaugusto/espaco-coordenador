using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;
using System.Data.Objects.DataClasses;

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

        public RESPOSTA ConsultarById(int idResposta)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.RESPOSTA.First(rs => rs.ID_RESPOSTA == idResposta);
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

        public List<RESPOSTA> ConsultarByProva(int idProva)
        {
            using (ECEntities db = new ECEntities())
            {
                var r = db.RESPOSTA.Where(rs => rs.QUESTAO.PROVA.ID_PROVA == idProva);

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

        public void Salvar(RESPOSTA r)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    //Salva a questão
                    db.RESPOSTA.Add(r);
                    db.SaveChanges();
                    db.Dispose();
                }

            }
            catch (Exception e)
            {

            }
        }

        public void Atualiza(RESPOSTA r)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    //Salva a questão
                    db.RESPOSTA.Add(r);
                    db.SaveChanges();
                    db.Dispose();
                }

            }
            catch (Exception e)
            {

            }
        }

        public void Salvar(EntityCollection<RESPOSTA> objetos, int idPergunta)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    foreach (var tipo in objetos)
                    {
                        RESPOSTA obj = new RESPOSTA();

                        if (tipo.ID_RESPOSTA > 0)
                            obj = db.RESPOSTA.First(rs => rs.ID_RESPOSTA == tipo.ID_RESPOSTA);
                        else
                            obj.ID_RESPOSTA = idPergunta;
                        
                        obj.TEXTO = tipo.TEXTO;
                        obj.RESPOSTA_CORRETA = tipo.RESPOSTA_CORRETA;

                        if (tipo.ID_RESPOSTA > 0)
                            db.SaveChanges();
                        else
                        {
                            db.RESPOSTA.Add(obj);
                            db.SaveChanges();
                            
                        }
                    }
                    db.Dispose();
                }

            }
            catch (Exception e)
            {

            }
        }
    }
}
