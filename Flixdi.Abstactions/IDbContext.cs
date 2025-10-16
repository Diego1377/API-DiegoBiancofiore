namespace Flixdi.Abstactions
{
    public  interface IDbContext<T> : IDbOperation<T> where T : class
    {

    }
}
