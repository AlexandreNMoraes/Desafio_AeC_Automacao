using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio_AeC_Automacao.BancoDados.Models
{
    [Table("Tb_Cursos")]
    public class ClsCursos
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Professor { get; set; }
        public string CargaHoraria { get; set; }
        public string Descricao { get; set; }
    }
}
