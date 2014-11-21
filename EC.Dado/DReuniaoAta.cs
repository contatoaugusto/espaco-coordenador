using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;
using System.Transactions;

namespace EC.Dado
{
    public class DReuniaoAta
    {
        public List<REUNIAO_ATA> Consulta()
        {
            using (ECEntities db = new ECEntities())
            {
                var tp = db.REUNIAO_ATA.ToList();
                
                List<REUNIAO_ATA> ltReuniaoAta = new List<REUNIAO_ATA>();
                foreach (var tipo in tp)
                {
                    REUNIAO_ATA reuniaoAta = new REUNIAO_ATA();
                    reuniaoAta.ID_ATA = tipo.ID_ATA;
                    reuniaoAta.ID_REUNIAO = tipo.ID_REUNIAO;
                    reuniaoAta.ID_FUNCIONARIO_RESPOSAVEL = tipo.ID_FUNCIONARIO_RESPOSAVEL;
                    reuniaoAta.DATA_FECHAMENTO = tipo.DATA_FECHAMENTO;

                    ltReuniaoAta.Add(reuniaoAta);
                }

                return ltReuniaoAta;
            }
        }
        
        public REUNIAO_ATA ConsultarById(int idAta)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.REUNIAO_ATA.FirstOrDefault(rs => rs.ID_ATA == idAta);
            }
        }

        public REUNIAO_ATA ConsultarByReuniao(int idReuniao)
        {

            try
            {
                using (ECEntities db = new ECEntities())
                {
                    var ata = db.REUNIAO_ATA.First(rs => rs.ID_REUNIAO == idReuniao);
                    ata.REUNIAO = db.REUNIAO.First(rs=> rs.ID_REUNIAO == idReuniao);
                    ata.FUNCIONARIO = db.FUNCIONARIO.First(rs=> rs.ID_FUNCIONARIO == ata.ID_FUNCIONARIO_RESPOSAVEL);
                    ata.FUNCIONARIO.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == ata.FUNCIONARIO.ID_PESSOA); 
                    return ata;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void Salvar(REUNIAO_ATA r)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    //Salva a questão
                    db.REUNIAO_ATA.Add(r);
                    db.SaveChanges();
                    db.Dispose();
                }

            }
            catch (Exception e)
            {

            }
        }

        public void Atualiza(REUNIAO_ATA q)
        {
            using (ECEntities db = new ECEntities())
            {
                var original = db.REUNIAO_ATA.First(rs => rs.ID_ATA == q.ID_ATA);

                original.ID_FUNCIONARIO_RESPOSAVEL   = q.ID_FUNCIONARIO_RESPOSAVEL;
                original.DATA_FECHAMENTO = q.DATA_FECHAMENTO;

                db.SaveChanges();
            }
        }
    }
}