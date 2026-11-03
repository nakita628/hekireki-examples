package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TeamEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamEntity>(TeamTable)

    var name by TeamTable.name
    var slug by TeamTable.slug
    val users by UserTeamEntity referrersOn UserTeamTable.teamId
    val userDatarooms by UserDataroomEntity referrersOn UserDataroomTable.teamId
    val documents by DocumentEntity referrersOn DocumentTable.teamId
    val folders by FolderEntity referrersOn FolderTable.teamId
    val domains by DomainEntity referrersOn DomainTable.teamId
    val invitations by InvitationEntity referrersOn InvitationTable.teamId
    val sentEmails by SentEmailEntity referrersOn SentEmailTable.teamId
    val brands by BrandEntity referrersOn BrandTable.teamId
    var defaultBrandId by TeamTable.defaultBrandId
    var defaultBrand by BrandEntity optionalReferencedOn TeamTable.defaultBrandId
    val datarooms by DataroomEntity referrersOn DataroomTable.teamId
    val agreements by AgreementEntity referrersOn AgreementTable.teamId
    val viewerGroups by ViewerGroupEntity referrersOn ViewerGroupTable.teamId
    val visitorGroups by VisitorGroupEntity referrersOn VisitorGroupTable.teamId
    val viewers by ViewerEntity referrersOn ViewerTable.teamId
    val permissionGroups by PermissionGroupEntity referrersOn PermissionGroupTable.teamId
    val linkPresets by LinkPresetEntity referrersOn LinkPresetTable.teamId
    val incomingWebhooks by IncomingWebhookEntity referrersOn IncomingWebhookTable.teamId
    val restrictedTokens by RestrictedTokenEntity referrersOn RestrictedTokenTable.teamId
    val webhooks by WebhookEntity referrersOn WebhookTable.teamId
    val conversations by ConversationEntity referrersOn ConversationTable.teamId
    val dataroomQuestions by DataroomQuestionEntity referrersOn DataroomQuestionTable.teamId
    val dataroomFaqItems by DataroomFaqItemEntity referrersOn DataroomFaqItemTable.teamId
    val uploadedDocuments by DocumentUploadEntity referrersOn DocumentUploadTable.teamId
    val tag by TagEntity referrersOn TagTable.teamId
    val annotations by DocumentAnnotationEntity referrersOn DocumentAnnotationTable.teamId
    val redactionJobs by DocumentRedactionJobEntity referrersOn DocumentRedactionJobTable.teamId
    val installedIntegrations by InstalledIntegrationEntity referrersOn InstalledIntegrationTable.teamId
    val links by LinkEntity optionalReferrersOn LinkTable.teamId
    val views by ViewEntity optionalReferrersOn ViewTable.teamId
    val workflows by WorkflowEntity referrersOn WorkflowTable.teamId
    val taskLists by TaskListEntity referrersOn TaskListTable.teamId
    val tasks by TaskEntity referrersOn TaskTable.teamId
    var plan by TeamTable.plan
    var stripeId by TeamTable.stripeId
    var subscriptionId by TeamTable.subscriptionId
    var startsAt by TeamTable.startsAt
    var endsAt by TeamTable.endsAt
    var pausedAt by TeamTable.pausedAt
    var pauseStartsAt by TeamTable.pauseStartsAt
    var pauseEndsAt by TeamTable.pauseEndsAt
    var cancelledAt by TeamTable.cancelledAt
    var trialStartedAt by TeamTable.trialStartedAt
    var trialEndsAt by TeamTable.trialEndsAt
    var trialReminderRunId by TeamTable.trialReminderRunId
    var trialExpiredRunId by TeamTable.trialExpiredRunId
    var limits by TeamTable.limits
    var enableExcelAdvancedMode by TeamTable.enableExcelAdvancedMode
    var replicateDataroomFolders by TeamTable.replicateDataroomFolders
    var timezone by TeamTable.timezone
    var ssoEnabled by TeamTable.ssoEnabled
    var ssoEmailDomain by TeamTable.ssoEmailDomain
    var ssoEnforcedAt by TeamTable.ssoEnforcedAt
    var surveyData by TeamTable.surveyData
    var agentsEnabled by TeamTable.agentsEnabled
    var vectorStoreId by TeamTable.vectorStoreId
    val chats by ChatEntity referrersOn ChatTable.teamId
    var createdAt by TeamTable.createdAt
    var updatedAt by TeamTable.updatedAt
    var ignoredDomains by TeamTable.ignoredDomains
    var globalBlockList by TeamTable.globalBlockList
    val notificationPreferences by NotificationPreferenceEntity referrersOn NotificationPreferenceTable.teamId
    var referralTrack by TeamTable.referralTrack
    var referralTrackSelectedAt by TeamTable.referralTrackSelectedAt
    var referralProfile by TeamTable.referralProfile
    val referralInvites by ReferralInviteEntity optionalReferrersOn ReferralInviteTable.teamId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TeamTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
