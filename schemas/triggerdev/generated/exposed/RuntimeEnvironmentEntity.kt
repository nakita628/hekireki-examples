package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RuntimeEnvironmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RuntimeEnvironmentEntity>(RuntimeEnvironmentTable)

    var slug by RuntimeEnvironmentTable.slug
    var apiKey by RuntimeEnvironmentTable.apiKey
    var rootApiKeyHiddenAt by RuntimeEnvironmentTable.rootApiKeyHiddenAt
    var pkApiKey by RuntimeEnvironmentTable.pkApiKey
    var type by RuntimeEnvironmentTable.type
    var isBranchableEnvironment by RuntimeEnvironmentTable.isBranchableEnvironment
    var branchName by RuntimeEnvironmentTable.branchName
    var parentEnvironment by RuntimeEnvironmentEntity optionalReferencedOn RuntimeEnvironmentTable.parentEnvironmentId
    var parentEnvironmentId by RuntimeEnvironmentTable.parentEnvironmentId
    val childEnvironments by RuntimeEnvironmentEntity optionalReferrersOn RuntimeEnvironmentTable.parentEnvironmentId
    var previewAutoArchiveAfterDays by RuntimeEnvironmentTable.previewAutoArchiveAfterDays
    var previewAutoArchiveExcludedBranches by RuntimeEnvironmentTable.previewAutoArchiveExcludedBranches
    var previewAutoArchiveNextCheckAt by RuntimeEnvironmentTable.previewAutoArchiveNextCheckAt
    var previewAutoArchiveCursorCreatedAt by RuntimeEnvironmentTable.previewAutoArchiveCursorCreatedAt
    var previewAutoArchiveCursorId by RuntimeEnvironmentTable.previewAutoArchiveCursorId
    var git by RuntimeEnvironmentTable.git
    var archivedAt by RuntimeEnvironmentTable.archivedAt
    var shortcode by RuntimeEnvironmentTable.shortcode
    var maximumConcurrencyLimit by RuntimeEnvironmentTable.maximumConcurrencyLimit
    var concurrencyLimitBurstFactor by RuntimeEnvironmentTable.concurrencyLimitBurstFactor
    var paused by RuntimeEnvironmentTable.paused
    var pauseSource by RuntimeEnvironmentTable.pauseSource
    var autoEnableInternalSources by RuntimeEnvironmentTable.autoEnableInternalSources
    var organization by OrganizationEntity referencedOn RuntimeEnvironmentTable.organizationId
    var organizationId by RuntimeEnvironmentTable.organizationId
    var project by ProjectEntity referencedOn RuntimeEnvironmentTable.projectId
    var projectId by RuntimeEnvironmentTable.projectId
    var orgMember by OrgMemberEntity optionalReferencedOn RuntimeEnvironmentTable.orgMemberId
    var orgMemberId by RuntimeEnvironmentTable.orgMemberId
    var createdAt by RuntimeEnvironmentTable.createdAt
    var updatedAt by RuntimeEnvironmentTable.updatedAt
    var builtInEnvironmentVariableOverrides by RuntimeEnvironmentTable.builtInEnvironmentVariableOverrides
    var tunnelId by RuntimeEnvironmentTable.tunnelId
    val backgroundWorkers by BackgroundWorkerEntity referrersOn BackgroundWorkerTable.runtimeEnvironmentId
    val backgroundWorkerTasks by BackgroundWorkerTaskEntity referrersOn BackgroundWorkerTaskTable.runtimeEnvironmentId
    val taskRuns by TaskRunEntity referrersOn TaskRunTable.runtimeEnvironmentId
    val taskQueues by TaskQueueEntity referrersOn TaskQueueTable.runtimeEnvironmentId
    val environmentVariableValues by EnvironmentVariableValueEntity referrersOn EnvironmentVariableValueTable.environmentId
    val checkpoints by CheckpointEntity referrersOn CheckpointTable.runtimeEnvironmentId
    val workerDeployments by WorkerDeploymentEntity referrersOn WorkerDeploymentTable.environmentId
    val workerDeploymentPromotions by WorkerDeploymentPromotionEntity referrersOn WorkerDeploymentPromotionTable.environmentId
    val taskRunAttempts by TaskRunAttemptEntity referrersOn TaskRunAttemptTable.runtimeEnvironmentId
    val checkpointRestoreEvent by CheckpointRestoreEventEntity referrersOn CheckpointRestoreEventTable.runtimeEnvironmentId
    val taskScheduleInstances by TaskScheduleInstanceEntity referrersOn TaskScheduleInstanceTable.environmentId
    val alerts by ProjectAlertEntity referrersOn ProjectAlertTable.environmentId
    val sessions by RuntimeEnvironmentSessionEntity referrersOn RuntimeEnvironmentSessionTable.environmentId
    var currentSession by RuntimeEnvironmentSessionEntity optionalReferencedOn RuntimeEnvironmentTable.currentSessionId
    var currentSessionId by RuntimeEnvironmentTable.currentSessionId
    val taskRunNumberCounter by TaskRunNumberCounterEntity referrersOn TaskRunNumberCounterTable.environmentId
    val taskRunCheckpoints by TaskRunCheckpointEntity referrersOn TaskRunCheckpointTable.runtimeEnvironmentId
    val waitpoints by WaitpointEntity referrersOn WaitpointTable.environmentId
    val workerInstances by WorkerInstanceEntity optionalReferrersOn WorkerInstanceTable.environmentId
    val waitpointTags by WaitpointTagEntity referrersOn WaitpointTagTable.environmentId
    val bulkActionGroup by BulkActionGroupEntity optionalReferrersOn BulkActionGroupTable.environmentId
    val customerQueries by CustomerQueryEntity optionalReferrersOn CustomerQueryTable.environmentId
    val prompts by PromptEntity referrersOn PromptTable.runtimeEnvironmentId
    val playgroundConversations by PlaygroundConversationEntity referrersOn PlaygroundConversationTable.runtimeEnvironmentId
    val errorGroupStates by ErrorGroupStateEntity optionalReferrersOn ErrorGroupStateTable.environmentId
    val taskIdentifiers by TaskIdentifierEntity referrersOn TaskIdentifierTable.runtimeEnvironmentId
    val apiKeys by ApiKeyEntity referrersOn ApiKeyTable.runtimeEnvironmentId
    val revokedApiKeys by RevokedApiKeyEntity referrersOn RevokedApiKeyTable.runtimeEnvironmentId
    val trustedDeployGrant by TrustedDeployEnvironmentEntity optionalBackReferencedOn TrustedDeployEnvironmentTable.runtimeEnvironmentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RuntimeEnvironmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
