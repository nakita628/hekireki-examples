package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserEntity>(UserTable)

    var name by UserTable.name
    var email by UserTable.email
    var emailVerified by UserTable.emailVerified
    var password by UserTable.password
    var sessionsExpiredAt by UserTable.sessionsExpiredAt
    var image by UserTable.image
    var admin by UserTable.admin
    var v4BetaEnabled by UserTable.v4BetaEnabled
    var supportAccess by UserTable.supportAccess
    val accounts by AccountEntity referrersOn AccountTable.userId
    val sessions by SessionEntity referrersOn SessionTable.userId
    val organizationMemberships by OrganizationMembershipEntity referrersOn OrganizationMembershipTable.userId
    val projectMemberships by ProjectMembershipEntity referrersOn ProjectMembershipTable.userId
    val invitations by MembershipInvitationEntity optionalReferrersOn MembershipInvitationTable.invitedByUserId
    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt
    var featureFlags by UserTable.featureFlags
    val annotatedLockedItem by AnnotationQueueItemEntity optionalReferrersOn AnnotationQueueItemTable.lockedByUserId
    val annotatedCompletedItem by AnnotationQueueItemEntity optionalReferrersOn AnnotationQueueItemTable.annotatorUserId
    val dashboardWidgetsCreated by DashboardWidgetEntity optionalReferrersOn DashboardWidgetTable.createdBy
    val dashboardWidgetsUpdated by DashboardWidgetEntity optionalReferrersOn DashboardWidgetTable.updatedBy
    val dashboardCreated by DashboardEntity optionalReferrersOn DashboardTable.createdBy
    val dashboardUpdated by DashboardEntity optionalReferrersOn DashboardTable.updatedBy
    val tableViewPresetCreated by TableViewPresetEntity optionalReferrersOn TableViewPresetTable.createdBy
    val tableViewPresetUpdated by TableViewPresetEntity optionalReferrersOn TableViewPresetTable.updatedBy
    val monitorsCreated by MonitorEntity optionalReferrersOn MonitorTable.createdBy
    val monitorsUpdated by MonitorEntity optionalReferrersOn MonitorTable.updatedBy
    val topicConfigsCreated by TopicConfigEntity optionalReferrersOn TopicConfigTable.createdBy
    val topicConfigsUpdated by TopicConfigEntity optionalReferrersOn TopicConfigTable.updatedBy
    val tracingReadinessCreated by TracingReadinessSettingsEntity optionalReferrersOn TracingReadinessSettingsTable.createdBy
    val tracingReadinessUpdated by TracingReadinessSettingsEntity optionalReferrersOn TracingReadinessSettingsTable.updatedBy
    val annotationQueueAssignment by AnnotationQueueAssignmentEntity referrersOn AnnotationQueueAssignmentTable.userId
    val surveys by SurveyEntity optionalReferrersOn SurveyTable.userId
    val commentReactions by CommentReactionEntity referrersOn CommentReactionTable.userId
    val notificationPreferences by NotificationPreferenceEntity referrersOn NotificationPreferenceTable.userId
    val defaultViews by DefaultViewEntity optionalReferrersOn DefaultViewTable.userId
    val inAppAgentConversations by InAppAgentConversationEntity optionalReferrersOn InAppAgentConversationTable.createdByUserId
    val inAppAgentRuns by InAppAgentRunEntity optionalReferrersOn InAppAgentRunTable.triggeredByUserId
    val apiKeysCreated by ApiKeyEntity optionalReferrersOn ApiKeyTable.createdByUserId
    val apiKeys by ApiKeyEntity optionalReferrersOn ApiKeyTable.userId
    val roleAssignments by RoleAssignmentEntity optionalReferrersOn RoleAssignmentTable.userId
    val evaluatorsCreated by EvaluatorEntity optionalReferrersOn EvaluatorTable.createdByUserId
    val evaluatorVersionsCreated by EvaluatorVersionEntity optionalReferrersOn EvaluatorVersionTable.createdByUserId
    val evaluationRulesCreated by EvaluationRuleEntity optionalReferrersOn EvaluationRuleTable.createdByUserId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
