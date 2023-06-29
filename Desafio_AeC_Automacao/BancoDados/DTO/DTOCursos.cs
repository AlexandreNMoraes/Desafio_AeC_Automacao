
using Desafio_AeC_Automacao.BancoDados.Context;
using Desafio_AeC_Automacao.BancoDados.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_AeC_Automacao.BancoDados.DTO
{
    public class DTOCursos
    {
        MySqlContext db = new MySqlContext();
        public List<ClsCursos> GetCursos()
        {
            return db.ClsCursos.ToList();
        }
        public void InsertCursos(ClsCursos Obj)
        {
            if (CursoRepetido(Obj)) return;
            db.Add(Obj);
            db.SaveChanges();
            Console.WriteLine("Dados Inseridos");
        }
        private bool CursoRepetido(ClsCursos Obj)
        {
            bool result = false;
            List<ClsCursos> Lista = GetCursos();
            if (Lista.Where(Item => Item.Titulo == Obj.Titulo).ToList().Count() > 0) {
                Console.WriteLine("Curso já registrado. Continuando...");
                result = true;
            }
            return result;
        }
        
    }
}
