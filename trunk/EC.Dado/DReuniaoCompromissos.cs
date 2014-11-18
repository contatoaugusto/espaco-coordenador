using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DReuniaoCompromissos
    {
        public List<REUNIAO_COMPROMISSO> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                return db.REUNIAO_COMPROMISSO.ToList();
            }
        }

       
        public REUNIAO_COMPROMISSO ConsultarById(int id)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.REUNIAO_COMPROMISSO.First(rs => rs.ID_COMPROMISSO == id);
            }
        }

        public List<REUNIAO_COMPROMISSO> ConsultarByReuniao(int idReuniao)
        {
            using (ECEntities db = new ECEntities())
            {
                var listResult = db.REUNIAO_COMPROMISSO.Where(rs => rs.ID_REUNIAO == idReuniao);

                List<REUNIAO_COMPROMISSO> ltCompromisso = new List<REUNIAO_COMPROMISSO>();

                foreach (var tipo in listResult)
                {
                    if (tipo.ID_COMPROMISSO != null && tipo.ID_COMPROMISSO != 0)
                    {
                        REUNIAO_COMPROMISSO compromisso = new REUNIAO_COMPROMISSO();
                        compromisso.ID_COMPROMISSO = tipo.ID_COMPROMISSO;
                        compromisso.ID_REUNIAO = tipo.ID_REUNIAO;
                        compromisso.ID_PESSOA = tipo.ID_PESSOA;
                        compromisso.DESCRICAO = tipo.DESCRICAO;
                        compromisso.ITEM = tipo.ITEM;
                        compromisso.DATA = tipo.DATA;
                        

                        compromisso.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == tipo.ID_PESSOA);

                        ltCompromisso.Add(compromisso);
                    }
                }

                return ltCompromisso;
            }
        }


        public void ExcluiCompromisso(int idCompromisso)
        {
            using (ECEntities db = new ECEntities())
            {
                var originalCompromisso = db.REUNIAO_COMPROMISSO.First(rs => rs.ID_COMPROMISSO == idCompromisso);
                if (originalCompromisso != null)
                {
                    db.REUNIAO_COMPROMISSO.Remove(originalCompromisso);
                    db.SaveChanges();
                }
            }
        }

        public void Salvar(REUNIAO_COMPROMISSO r)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    //Salva a questão
                    db.REUNIAO_COMPROMISSO.Add(r);
                    db.SaveChanges();
                    db.Dispose();
                }

            }
            catch (Exception e)
            {

            }
        }

        public void Salvar(List<REUNIAO_COMPROMISSO> objetos)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    foreach (var tipo in objetos)
                    {
                        REUNIAO_COMPROMISSO obj = new REUNIAO_COMPROMISSO();

                        if (tipo.ID_COMPROMISSO > 0)
                            obj = db.REUNIAO_COMPROMISSO.First(rs => rs.ID_COMPROMISSO == tipo.ID_COMPROMISSO);

                        obj.ID_REUNIAO = tipo.ID_REUNIAO;
                        obj.ID_PESSOA = tipo.ID_PESSOA;
                        obj.DESCRICAO = tipo.DESCRICAO;
                        obj.ITEM = tipo.ITEM;
                        obj.DATA = tipo.DATA;
                        
                        Salvar(obj);
                    }
                }

            }
            catch (Exception e)
            {

            }
        }
    }
}