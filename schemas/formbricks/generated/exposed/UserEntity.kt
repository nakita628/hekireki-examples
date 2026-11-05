package formbricks.models

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

    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt
    var name by UserTable.name
    var email by UserTable.email
    var emailVerified by UserTable.emailVerified
    var twoFactorSecret by UserTable.twoFactorSecret
    var twoFactorEnabled by UserTable.twoFactorEnabled
    var backupCodes by UserTable.backupCodes
    var password by UserTable.password
    var identityProvider by UserTable.identityProvider
    var identityProviderAccountId by UserTable.identityProviderAccountId
    val memberships by MembershipEntity referrersOn MembershipTable.userId
    val accounts by AccountEntity referrersOn AccountTable.userId
    val sessions by SessionEntity referrersOn SessionTable.userId
    val twoFactor by TwoFactorEntity optionalBackReferencedOn TwoFactorTable.userId
    val oauthClients by OauthClientEntity optionalReferrersOn OauthClientTable.userId
    val oauthAccessTokens by OauthAccessTokenEntity optionalReferrersOn OauthAccessTokenTable.userId
    val oauthRefreshTokens by OauthRefreshTokenEntity referrersOn OauthRefreshTokenTable.userId
    val oauthConsents by OauthConsentEntity optionalReferrersOn OauthConsentTable.userId
    val passwordResetToken by PasswordResetTokenEntity optionalBackReferencedOn PasswordResetTokenTable.userId
    var groupId by UserTable.groupId
    val invitesCreated by InviteEntity referrersOn InviteTable.creatorId
    val invitesAccepted by InviteEntity optionalReferrersOn InviteTable.acceptorId
    var notificationSettings by UserTable.notificationSettings
    var locale by UserTable.locale
    val surveys by SurveyEntity optionalReferrersOn SurveyTable.createdBy
    val ownedSurveys by SurveyEntity optionalReferrersOn SurveyTable.ownerId
    val charts by ChartEntity optionalReferrersOn ChartTable.createdBy
    val dashboards by DashboardEntity optionalReferrersOn DashboardTable.createdBy
    val creatives by CreativeEntity optionalReferrersOn CreativeTable.createdBy
    val feedbackSources by FeedbackSourceEntity optionalReferrersOn FeedbackSourceTable.createdBy
    val teamUsers by TeamUserEntity referrersOn TeamUserTable.userId
    val workflows by WorkflowEntity optionalReferrersOn WorkflowTable.createdBy
    val workflowVersionsPublished by WorkflowVersionEntity optionalReferrersOn WorkflowVersionTable.publishedBy
    val retentionPoliciesUpdated by RetentionPolicyEntity optionalReferrersOn RetentionPolicyTable.updatedById
    val exemptionsCreated by RetentionExemptionEntity optionalReferrersOn RetentionExemptionTable.createdById
    val exemptionsRevoked by RetentionExemptionEntity optionalReferrersOn RetentionExemptionTable.revokedById
    val retentionNotices by RetentionNoticeEntity optionalReferrersOn RetentionNoticeTable.userId
    val attachmentExports by AttachmentExportEntity optionalReferrersOn AttachmentExportTable.requestedById
    var lastLoginAt by UserTable.lastLoginAt
    var isActive by UserTable.isActive
    var reactivatedAt by UserTable.reactivatedAt
    var lastActiveAt by UserTable.lastActiveAt
    var isBootstrapAdmin by UserTable.isBootstrapAdmin

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
