package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProjectEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProjectEntity>(ProjectTable)

    var slug by ProjectTable.slug
    var name by ProjectTable.name
    var externalRef by ProjectTable.externalRef
    var organization by OrganizationEntity referencedOn ProjectTable.organizationId
    var organizationId by ProjectTable.organizationId
    var createdAt by ProjectTable.createdAt
    var updatedAt by ProjectTable.updatedAt
    var deletedAt by ProjectTable.deletedAt
    var initializedAt by ProjectTable.initializedAt
    var defaultRuntime by ProjectTable.defaultRuntime
    var version by ProjectTable.version
    var engine by ProjectTable.engine
    var builderProjectId by ProjectTable.builderProjectId
    val workerGroups by WorkerInstanceGroupEntity optionalReferrersOn WorkerInstanceGroupTable.projectId
    val workers by WorkerInstanceEntity optionalReferrersOn WorkerInstanceTable.projectId
    var defaultWorkerGroup by WorkerInstanceGroupEntity optionalReferencedOn ProjectTable.defaultWorkerGroupId
    var defaultWorkerGroupId by ProjectTable.defaultWorkerGroupId
    var allowedWorkerQueues by ProjectTable.allowedWorkerQueues
    val environments by RuntimeEnvironmentEntity referrersOn RuntimeEnvironmentTable.projectId
    val backgroundWorkers by BackgroundWorkerEntity referrersOn BackgroundWorkerTable.projectId
    val backgroundWorkerTasks by BackgroundWorkerTaskEntity referrersOn BackgroundWorkerTaskTable.projectId
    val taskRuns by TaskRunEntity referrersOn TaskRunTable.projectId
    val runTags by TaskRunTagEntity referrersOn TaskRunTagTable.projectId
    val taskQueues by TaskQueueEntity referrersOn TaskQueueTable.projectId
    val environmentVariables by EnvironmentVariableEntity referrersOn EnvironmentVariableTable.projectId
    val checkpoints by CheckpointEntity referrersOn CheckpointTable.projectId
    val workerDeployment by WorkerDeploymentEntity referrersOn WorkerDeploymentTable.projectId
    val checkpointRestoreEvent by CheckpointRestoreEventEntity referrersOn CheckpointRestoreEventTable.projectId
    val taskSchedules by TaskScheduleEntity referrersOn TaskScheduleTable.projectId
    val alertChannels by ProjectAlertChannelEntity referrersOn ProjectAlertChannelTable.projectId
    val alerts by ProjectAlertEntity referrersOn ProjectAlertTable.projectId
    val alertStorages by ProjectAlertStorageEntity referrersOn ProjectAlertStorageTable.projectId
    val bulkActionGroups by BulkActionGroupEntity referrersOn BulkActionGroupTable.projectId
    val backgroundWorkerFile by BackgroundWorkerFileEntity referrersOn BackgroundWorkerFileTable.projectId
    val waitpoints by WaitpointEntity referrersOn WaitpointTable.projectId
    val taskRunWaitpoints by TaskRunWaitpointEntity referrersOn TaskRunWaitpointTable.projectId
    val taskRunCheckpoints by TaskRunCheckpointEntity referrersOn TaskRunCheckpointTable.projectId
    val waitpointTags by WaitpointTagEntity referrersOn WaitpointTagTable.projectId
    val connectedGithubRepository by ConnectedGithubRepositoryEntity optionalBackReferencedOn ConnectedGithubRepositoryTable.projectId
    val trustedDeployRepository by TrustedDeployRepositoryEntity optionalBackReferencedOn TrustedDeployRepositoryTable.projectId
    val organizationProjectIntegration by OrganizationProjectIntegrationEntity referrersOn OrganizationProjectIntegrationTable.projectId
    val customerQueries by CustomerQueryEntity optionalReferrersOn CustomerQueryTable.projectId
    val playgroundConversations by PlaygroundConversationEntity referrersOn PlaygroundConversationTable.projectId
    var buildSettings by ProjectTable.buildSettings
    var onboardingData by ProjectTable.onboardingData
    val taskScheduleInstances by TaskScheduleInstanceEntity referrersOn TaskScheduleInstanceTable.projectId
    val metricsDashboards by MetricsDashboardEntity optionalReferrersOn MetricsDashboardTable.projectId
    val llmModels by LlmModelEntity optionalReferrersOn LlmModelTable.projectId
    val prompts by PromptEntity referrersOn PromptTable.projectId
    val platformNotifications by PlatformNotificationEntity optionalReferrersOn PlatformNotificationTable.projectId
    val errorGroupStates by ErrorGroupStateEntity referrersOn ErrorGroupStateTable.projectId
    val taskIdentifiers by TaskIdentifierEntity referrersOn TaskIdentifierTable.projectId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProjectTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
