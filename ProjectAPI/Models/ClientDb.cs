using ProjectJson.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace ProjectAPI.Models
{
    public class ClientDb : IClientMessage
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
        public string? City { get; set; }
        public string? Cargo { get; set; }
        public string? Tel { get; set; }

        public List<OrderListDb>? OrderLists { get; set; } = new();
    }
}
