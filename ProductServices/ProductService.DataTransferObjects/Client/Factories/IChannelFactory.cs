namespace ProductService.DataTransfer.Client.Factories
{
    public interface IChannelFactory
    {
        IListener CreateListener();
        IPublisher CreatePublisher();

    }
}