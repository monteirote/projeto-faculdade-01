using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto01.ViewModels
{
    public class CreateDoctorViewModel {

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Specialty { get; set; } = string.Empty;

        public string ProfilePicture { get; set; } = string.Empty;
    }
}
