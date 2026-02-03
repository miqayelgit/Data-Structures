using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures.Exercise
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsMarried { get; set; }

        public bool IsDeleted { get; set; } = false;


        public override string ToString()
        {
            return $"[Id:{this.Id}, First Name:{this.Name}, " +
                $"Last Name:{this.LastName}, Age:{this.Age}, " +
                $"Email:{this.Email}, IsMarried:{this.IsMarried}, IsDeleted:{IsDeleted}]";
        }
    }
}
