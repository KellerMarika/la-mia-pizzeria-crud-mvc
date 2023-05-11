using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
   
        public interface IHasUpdate
        {
            public DateTime? UpdateDate { get; set; }
        }
        public static class Extension
        {
            public static void UpdateAndSave<T>(this T row) where T : IHasUpdate
            {
                row.UpdateDate = DateTime.UtcNow;
                //SQL salva riga
            }
        }
        public class Timestamps : IHasUpdate
        {
            [Column("created_at")]
            public DateTime CreationDate { get; set; } = DateTime.UtcNow;
            [Column("updated_at")]
            public DateTime? UpdateDate { get; set; } = null;

            public void Save() { }

            public Timestamps()
            {
                this.UpdateAndSave();
            }
        }
   

}
