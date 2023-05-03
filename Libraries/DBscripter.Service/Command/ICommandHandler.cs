namespace DBScripter.Service.Command
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }

}
