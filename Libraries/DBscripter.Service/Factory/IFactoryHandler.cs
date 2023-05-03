namespace DBScripter.Service.Factory
{
    public interface IFactoryHandler<TFactory, TProductInterface>
    {
            TProductInterface Handle(TFactory factory );
    }
}
