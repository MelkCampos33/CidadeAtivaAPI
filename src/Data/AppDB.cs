using CidadeAtivaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CidadeAtivaApi.Data
{
    public class AppDB(DbContextOptions<AppDB> options) : DbContext(options) // DbContext vem do ASP.NET - O AppDB é o banco local
    {
        // Essa classe conecta o código ao banco de dados (nesse caso ainda está sem SQL Server)
        // Nessa versão está usando "inMemory"

        // Usando o Framework Entity
        // Permite trabalhar os dados relacionais usando classes e objetos em vez de escrever SQL
        public DbSet<ProblamasUrbano> Problamas => Set<ProblamasUrbano>();
    }
}
