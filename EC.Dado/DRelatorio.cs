using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Dado
{
    public class DRelatorio
    {
        public void RelatorioVagas()
        {
            using (ECEntities db = new ECEntities())
            {
                List<Modelo.RelatorioVagas> listarel = new List<EC.Modelo.RelatorioVagas>();
                var dis = db.DISCIPLINA.ToList();
                foreach (var d in dis)
                {
                    Modelo.RelatorioVagas rel = new Modelo.RelatorioVagas();
                    rel.Disciplina = new DISCIPLINA();
                    rel.Turma = new List<TURMA>();
                    rel.Disciplina = d;
                    var turma = db.TURMA.Where(rs => rs.PERIODO_CURSO == d.PERIODO_CURSO).ToList();
                    foreach (var t in turma)
                    {
                        rel.Turma.Add(t);
                    }
                }

            }
        }
    }
}
