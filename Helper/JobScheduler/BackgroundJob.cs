using AltaProject.Entity;
using AltaProject.Model.EntityModel;
using AltaProject.Repository;
using Quartz;

namespace AltaProject.Helper.JobScheduler
{
    [DisallowConcurrentExecution]
    public class BackgroundJob : IJob
    {
        private readonly INotificationRepository notificationRepository;
        private readonly ILogger<BackgroundJob> _logger;

        public BackgroundJob(INotificationRepository notificationRepository, ILogger<BackgroundJob> logger)
        {
            this.notificationRepository = notificationRepository;
            this._logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            //Task execution
            await SomeTask();

        }
        private async Task SomeTask()
        {
            //Perform background task
            /*var response = await notificationRepository.sendNotificationAsync(notificationModel);*/
            var response = await notificationRepository.sendNotificationBeforeEventStarted();
            _logger.LogInformation(response.message);
        }

    }
}
