namespace ProductService.DataTransfer.Channel.Factories
{
    public interface IChannelFactory
    {
        string ConnectionString { get; set; }
        string ChannelName { get; set; }
        IListener CreateListener();
        IPublisher CreatePublisher();

    }
}