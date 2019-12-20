using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")] //Serve para dizer que o campo é obrigatório
        // Serve para determinar o tamanho máximo e mínimo que o campo terá e pode usar diretamente uma mensagem de erro que você quiser usar
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")] //Serve para informar que o email foi escrito de forma errada
        [DataType(DataType.EmailAddress)] //Serve para transformar em link de redirecionamento para aplicações de emails
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")] //Serve para criar um alias para a variável BirthDate
        [DataType(DataType.Date)] //Serve para padronizar o tipo de data que queria que apareça
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")] //Sever para padronizar o formato de como deve ser escrito o campo dentro de um teto mínimo e um máximo
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")] //Serve para determinar quantas casas decimais iram aparecer
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }

    }
}
