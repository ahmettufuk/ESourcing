namespace ESourcing.Order.Settings.RabbitMQSettings
{
    public class EventBus :IEventBus
    {
        /*
           "EventBus": {
    "HostName": "localhost",
    "UserName": "guest",
    "Password": "guest",
    "RetryCount": 5
             }
         */

        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RetryCount { get; set; }

        
    }
}
