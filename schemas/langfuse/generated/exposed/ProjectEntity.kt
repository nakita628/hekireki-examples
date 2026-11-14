package langfuse.models

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

    var orgId by ProjectTable.orgId
    var createdAt by ProjectTable.createdAt
    var updatedAt by ProjectTable.updatedAt
    var deletedAt by ProjectTable.deletedAt
    var name by ProjectTable.name
    var retentionDays by ProjectTable.retentionDays
    var hasTraces by ProjectTable.hasTraces
    var metadata by ProjectTable.metadata
    var homeDashboardId by ProjectTable.homeDashboardId
    var homeDashboard by DashboardEntity optionalReferencedOn ProjectTable.homeDashboardId
    val projectMembers by ProjectMembershipEntity referrersOn ProjectMembershipTable.projectId
    var organization by OrganizationEntity referencedOn ProjectTable.orgId
    val apiKeys by ApiKeyEntity optionalReferrersOn ApiKeyTable.projectId
    val roleAssignments by RoleAssignmentEntity optionalReferrersOn RoleAssignmentTable.projectId
    val dataset by DatasetEntity referrersOn DatasetTable.projectId
    val invitations by MembershipInvitationEntity optionalReferrersOn MembershipInvitationTable.projectId
    val sessions by TraceSessionEntity referrersOn TraceSessionTable.projectId
    val prompt by PromptEntity referrersOn PromptTable.projectId
    val skill by SkillEntity referrersOn SkillTable.projectId
    val skillFile by SkillFileEntity referrersOn SkillFileTable.projectId
    val skillBlob by SkillBlobEntity referrersOn SkillBlobTable.projectId
    val model by ModelEntity optionalReferrersOn ModelTable.projectId
    val evalTemplate by EvalTemplateEntity optionalReferrersOn EvalTemplateTable.projectId
    val jobConfiguration by JobConfigurationEntity referrersOn JobConfigurationTable.projectId
    val jobExecution by JobExecutionEntity referrersOn JobExecutionTable.projectId
    val evaluator by EvaluatorEntity referrersOn EvaluatorTable.projectId
    val evaluationRule by EvaluationRuleEntity referrersOn EvaluationRuleTable.projectId
    val evaluationRuleEvaluatorAssignment by EvaluationRuleEvaluatorAssignmentEntity referrersOn
        EvaluationRuleEvaluatorAssignmentTable.projectId
    val tracingReadinessSettings by TracingReadinessSettingsEntity optionalBackReferencedOn TracingReadinessSettingsTable.id
    val llmApiKeys by LlmApiKeysEntity optionalReferrersOn LlmApiKeysTable.projectId
    val posthogIntegration by PosthogIntegrationEntity referrersOn PosthogIntegrationTable.id
    val mixpanelIntegration by MixpanelIntegrationEntity referrersOn MixpanelIntegrationTable.id
    val blobStorageIntegration by BlobStorageIntegrationEntity referrersOn BlobStorageIntegrationTable.id
    val externalMediaStorageIntegration by ExternalMediaStorageIntegrationEntity optionalBackReferencedOn
        ExternalMediaStorageIntegrationTable.id
    val webCalloutEndpoints by WebCalloutEndpointEntity referrersOn WebCalloutEndpointTable.projectId
    val scoreConfig by ScoreConfigEntity referrersOn ScoreConfigTable.projectId
    val batchExport by BatchExportEntity referrersOn BatchExportTable.projectId
    val batchAction by BatchActionEntity referrersOn BatchActionTable.projectId
    val comment by CommentEntity referrersOn CommentTable.projectId
    val commentReactions by CommentReactionEntity referrersOn CommentReactionTable.projectId
    val annotationQueue by AnnotationQueueEntity referrersOn AnnotationQueueTable.projectId
    val annotationQueueItem by AnnotationQueueItemEntity referrersOn AnnotationQueueItemTable.projectId
    val media by MediaEntity referrersOn MediaTable.projectId
    val promptDependency by PromptDependencyEntity referrersOn PromptDependencyTable.projectId
    val llmSchema by LlmSchemaEntity referrersOn LlmSchemaTable.projectId
    val llmTool by LlmToolEntity referrersOn LlmToolTable.projectId
    val promptProtectedLabels by PromptProtectedLabelsEntity referrersOn PromptProtectedLabelsTable.projectId
    val dashboard by DashboardEntity optionalReferrersOn DashboardTable.projectId
    val dashboardWidget by DashboardWidgetEntity optionalReferrersOn DashboardWidgetTable.projectId
    val tableViewPreset by TableViewPresetEntity referrersOn TableViewPresetTable.projectId
    val sessionViews by SessionViewEntity referrersOn SessionViewTable.projectId
    val sessionViewPinOverrides by SessionViewPinOverrideEntity referrersOn SessionViewPinOverrideTable.projectId
    val actions by ActionEntity referrersOn ActionTable.projectId
    val triggers by TriggerEntity referrersOn TriggerTable.projectId
    val automationExecutions by AutomationExecutionEntity referrersOn AutomationExecutionTable.projectId
    val automation by AutomationEntity referrersOn AutomationTable.projectId
    val monitors by MonitorEntity referrersOn MonitorTable.projectId
    val defaultLlmModel by DefaultLlmModelEntity referrersOn DefaultLlmModelTable.projectId
    val topicConfig by TopicConfigEntity optionalBackReferencedOn TopicConfigTable.projectId
    val price by PriceEntity optionalReferrersOn PriceTable.projectId
    val slackIntegration by SlackIntegrationEntity optionalBackReferencedOn SlackIntegrationTable.projectId
    val pendingDeletion by PendingDeletionEntity referrersOn PendingDeletionTable.projectId
    val annotationQueueAssignment by AnnotationQueueAssignmentEntity referrersOn AnnotationQueueAssignmentTable.projectId
    val notificationPreference by NotificationPreferenceEntity referrersOn NotificationPreferenceTable.projectId
    val defaultView by DefaultViewEntity referrersOn DefaultViewTable.projectId
    val inAppAgentConversation by InAppAgentConversationEntity referrersOn InAppAgentConversationTable.projectId
    val inAppAgentEvent by InAppAgentEventEntity referrersOn InAppAgentEventTable.projectId
    val inAppAgentRun by InAppAgentRunEntity referrersOn InAppAgentRunTable.projectId
    val inAppAgentPendingToolApproval by InAppAgentPendingToolApprovalEntity referrersOn InAppAgentPendingToolApprovalTable.projectId
    val topicClusteringRuns by TopicClusteringRunEntity referrersOn TopicClusteringRunTable.projectId
    val gatewayConfigs by GatewayConfigEntity optionalReferrersOn GatewayConfigTable.defaultIngestionProjectId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProjectTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
