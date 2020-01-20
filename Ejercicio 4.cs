
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

/*
A partir de las clases CajaRepository y SucursalRepository, crear la clase BaseRepository<T> 
que unifique los métodos GetAllAsync y GetOneAsync
Crear un abstract BaseEntity que defina la property Id y luego modificar las entities Caja y Sucursal para que hereden de BaseEntity 
Aclaración: Se deben respetar la interfaces. 

***INTERFACES Y HERENCIAS***

    prueba
   
*/

namespace Domain.Entities
{
    public abstract class BaseEntity  // Clase Abstracta con su propiedad
    {
        private int idBase;

        public int IdBase
        {
            get
            {
                return idBase; 
            }
            set
            {
                idBase = value;
            }

        }
    }


    public class Caja : BaseEntity  // Hereda de la Clase Abstracta
    {
        
        public int SucursalId { get; }
        public string Descripcion { get; }
        public int TipoCajaId { get; }

        public Caja(int sucursalId, string descripcion, int tipoCajaId) : base (idBase)
        {
            
            SucursalId = sucursalId;   
            Descripcion = descripcion;
            TipoCajaId = tipoCajaId;
        }
    }

    public class Sucursal : BaseEntity // Hereda de la Clase Abstracta
    {
        
        public string Direccion { get; }
        public string Telefono { get; }

        public Sucursal(string direccion, string telefono) : base (idBase)
        {
            
            Direccion = direccion;
            Telefono = telefono;
        }
    }
}
        
namespace Infrastructure.Data.Repositories
{
	public interface ICajaRepository 
	{
		Task<IEnumerable<Caja>> GetAllAsync();
		Task<Caja> GetOneAsync(Guid id);
	}
	
	public interface ISucursalRepository
	{
		Task<IEnumerable<Sucursal>> GetAllAsync();
		Task<Sucursal> GetOneAsync(int id);
	}

   

    public class BaseRepository<TEntity, TEntity2>
                 where TEntity:CajaRepository,ICajaRepository, new()
                 where TEntity2:SucursalRepository,ISucursalRepository, new()
    {
        private readonly DataContext _db;
        public BaseRepository(DataContext _db)
                     => _db = db;
        
        Task<TEntity<IEnumerable<TEntity>>> GetAllAsync()
            => await _db.Cajas.ToListAsync();
        //
       
    }

    public class CajaRepository : ICajaRepository
    {
        private readonly DataContext _db;

        public CajaRepository(DataContext db)
            => _db = db;

        public async Task<IEnumerable<Caja>> GetAllAsync()
            => await _db.Cajas.ToListAsync();

        public async Task<Caja> GetOneAsync(Guid id)
            => await _db.Cajas.FirstOrDefaultAsync(x => x.Id == id);
    }

    public class SucursalRepository : ISucursalRepository
    {
        private readonly DataContext _db;

        public CajaRepository(DataContext db)
            => _db = db;

        public async Task<IEnumerable<Sucursal>> GetAllAsync()
            => await _db.Sucursales.ToListAsync();

        public async Task<Sucursal> GetOneAsync(int id)
            => await _db.Sucursales.FirstOrDefaultAsync(x => x.Id == id);
    }
}