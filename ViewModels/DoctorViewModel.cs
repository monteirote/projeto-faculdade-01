using System.ComponentModel.DataAnnotations;

namespace Projeto01.ViewModels
{
    public class CreateDoctorViewModel {

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Specialty { get; set; } = string.Empty;

        public string ProfilePicture { get; set; } = string.Empty;
    }

    public class GetDoctorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Specialty { get; set; } = string.Empty;

    }
}
