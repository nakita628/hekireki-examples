package triggerdev.models

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

    var email by UserTable.email
    var pendingEmail by UserTable.pendingEmail
    var authenticationMethod by UserTable.authenticationMethod
    var authenticationProfile by UserTable.authenticationProfile
    var authenticationExtraParams by UserTable.authenticationExtraParams
    var authIdentifier by UserTable.authIdentifier
    var displayName by UserTable.displayName
    var name by UserTable.name
    var avatarUrl by UserTable.avatarUrl
    var admin by UserTable.admin
    var dashboardPreferences by UserTable.dashboardPreferences
    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt
    var isOnCloudWaitlist by UserTable.isOnCloudWaitlist
    var featureCloud by UserTable.featureCloud
    var isOnHostedRepoWaitlist by UserTable.isOnHostedRepoWaitlist
    var marketingEmails by UserTable.marketingEmails
    var confirmedBasicDetails by UserTable.confirmedBasicDetails
    var referralSource by UserTable.referralSource
    var onboardingData by UserTable.onboardingData
    val orgMemberships by OrgMemberEntity referrersOn OrgMemberTable.userId
    val sentInvites by OrgMemberInviteEntity referrersOn OrgMemberInviteTable.inviterId
    var mfaEnabledAt by UserTable.mfaEnabledAt
    var mfaSecretReference by SecretReferenceEntity optionalReferencedOn UserTable.mfaSecretReferenceId
    var mfaSecretReferenceId by UserTable.mfaSecretReferenceId
    var mfaLastUsedCode by UserTable.mfaLastUsedCode
    var sessionDuration by UserTable.sessionDuration
    var nextSessionEnd by UserTable.nextSessionEnd
    var invitationCode by InvitationCodeEntity optionalReferencedOn UserTable.invitationCodeId
    var invitationCodeId by UserTable.invitationCodeId
    val personalAccessTokens by PersonalAccessTokenEntity referrersOn PersonalAccessTokenTable.userId
    val createdApiKeys by ApiKeyEntity optionalReferrersOn ApiKeyTable.createdByUserId
    val deployments by WorkerDeploymentEntity optionalReferrersOn WorkerDeploymentTable.triggeredById
    val backupCodes by MfaBackupCodeEntity referrersOn MfaBackupCodeTable.userId
    val bulkActions by BulkActionGroupEntity optionalReferrersOn BulkActionGroupTable.userId
    val impersonationsPerformed by ImpersonationAuditLogEntity referrersOn ImpersonationAuditLogTable.adminId
    val impersonationsReceived by ImpersonationAuditLogEntity referrersOn ImpersonationAuditLogTable.targetId
    val supportAccessRequestsMade by SupportAccessRequestEntity optionalReferrersOn SupportAccessRequestTable.requestedById
    val supportAccessRequestsApproved by SupportAccessRequestEntity optionalReferrersOn SupportAccessRequestTable.approvedById
    val customerQueries by CustomerQueryEntity optionalReferrersOn CustomerQueryTable.userId
    val metricsDashboards by MetricsDashboardEntity optionalReferrersOn MetricsDashboardTable.ownerId
    val platformNotifications by PlatformNotificationEntity optionalReferrersOn PlatformNotificationTable.userId
    val platformNotificationInteractions by PlatformNotificationInteractionEntity referrersOn PlatformNotificationInteractionTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
