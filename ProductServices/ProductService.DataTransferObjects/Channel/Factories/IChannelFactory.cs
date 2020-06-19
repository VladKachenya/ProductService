namespace ProductService.DataTransfer.Channel.Factories
{
    public interface IChannelFactory
    {
        IListener CreateListener();
        IPublisher CreatePublisher();

    }
}