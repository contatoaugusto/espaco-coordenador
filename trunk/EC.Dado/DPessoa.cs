using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Dado
{
    public class DPessoa
    {
        public List<PESSOA> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                var c = db.PESSOA.ToList().OrderBy(n => n.NOME);
                List<PESSOA> list = new List<PESSOA>();
                foreach (var tipo in c)
                {
                    PESSOA obj = new PESSOA();
                    obj.ID_PESSOA = tipo.ID_PESSOA;
                    obj.NOME = tipo.NOME;
                    obj.TELEFONE = tipo.TELEFONE;
                    obj.EMAIL = tipo.EMAIL;

                    list.Add(obj);
                }

                return list;
            }
        }

        public List<PESSOA> Consultar(string prefixText, int count)
        {
            using (ECEntities db = new ECEntities())
            {


                var c = db.PESSOA.Where(n => n.NOME.StartsWith(prefixText)).OrderBy(n => n.NOME);
                List<PESSOA> list = new List<PESSOA>();
                foreach (var tipo in c)
                {
                    PESSOA obj = new PESSOA();
                    obj.ID_PESSOA = tipo.ID_PESSOA;
                    obj.NOME = tipo.NOME;
                    obj.TELEFONE = tipo.TELEFONE;
                    obj.EMAIL = tipo.EMAIL;

                    list.Add(obj);
                }

                return list;
            }
        }
        
        public PESSOA ConsultarById(int id)
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    var obj = db.PESSOA.First(rs => rs.ID_PESSOA == id);

                    return obj;
                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }

    }
}
