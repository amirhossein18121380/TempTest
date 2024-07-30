namespace Application.Contracts.Persistence;

public interface IUnitOfWork
{
    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    public IProductRepository ProductRepository { get; }
    Task CommitAsync();
    ValueTask RollBackAsync();
}