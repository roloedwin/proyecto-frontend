using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<OrdenTrabajo> OrdenesTrabajo { get; set; }
    public DbSet<DetalleOrden> DetalleOrden { get; set; }
    public DbSet<Repuesto> Repuestos { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Facturacion> Facturacion { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    
}
