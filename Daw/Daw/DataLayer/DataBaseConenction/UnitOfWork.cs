using Daw.DataLayer.Repositories;

namespace Daw.DataLayer.DataBaseConenction
{
    public class UnitOfWork
    {
        private readonly AppDbContext _appContext;
        public UnitOfWork(AppDbContext appContext,
            ProductsRepository productsRepository, UserRepository userRepository, UserProductRepository userProductRepository)
        {
            _appContext = appContext;
            ProductsRepository = productsRepository;
            UserRepository = userRepository;
            UserProductRepository = userProductRepository;
        }

        public ProductsRepository ProductsRepository { get; }
        public UserRepository UserRepository { get; }
        public UserProductRepository UserProductRepository { get; }
        public async Task SaveChangesAsync()
        {
            try
            {
                await _appContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var errorMessage = "Error when saving to the database: "
                                   + $"{ex.Message}\n\n"
                                   + $"{ex.InnerException}\n\n"
                                   + $"{ex.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
            }
        }
    }
}
