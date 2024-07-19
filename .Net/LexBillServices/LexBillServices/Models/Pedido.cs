using System;
using System.ComponentModel.DataAnnotations;
public class Pedido
    {
    public int PedidoId { get; set; }

    [Required(ErrorMessage = "El ID del cliente es requerido")]
    [Display(Name = "ID del Cliente")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "La fecha es requerida")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El ITBMS es requerido")]
    [Range(0, double.MaxValue, ErrorMessage = "El ITBMS debe ser un valor positivo")]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
    public decimal ITBMS { get; set; }

    [Required(ErrorMessage = "El total es requerido")]
    [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un valor positivo")]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
    public decimal Total { get; set; }
}

