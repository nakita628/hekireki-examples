package inboxzero.models

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
    var image by UserTable.image
    val accounts by AccountEntity referrersOn AccountTable.userId
    val sessions by SessionEntity referrersOn SessionTable.userId
    var completedOnboardingAt by UserTable.completedOnboardingAt
    var signupIntent by UserTable.signupIntent
    var onboardingAnswers by UserTable.onboardingAnswers
    var lastLogin by UserTable.lastLogin
    var utms by UserTable.utms
    var errorMessages by UserTable.errorMessages
    var announcementDismissedAt by UserTable.announcementDismissedAt
    var dismissedHints by UserTable.dismissedHints
    var surveyFeatures by UserTable.surveyFeatures
    var surveyRole by UserTable.surveyRole
    var surveyGoal by UserTable.surveyGoal
    var surveyCompanySize by UserTable.surveyCompanySize
    var surveySource by UserTable.surveySource
    var surveyImprovements by UserTable.surveyImprovements
    var mcpServerEnabled by UserTable.mcpServerEnabled
    var mcpTokenVersion by UserTable.mcpTokenVersion
    var emailOtpEnabled by UserTable.emailOtpEnabled
    var emailOtpVersion by UserTable.emailOtpVersion
    var aiProvider by UserTable.aiProvider
    var aiModel by UserTable.aiModel
    var aiApiKey by UserTable.aiApiKey
    var decisionModelEnabled by UserTable.decisionModelEnabled
    var webhookSecret by UserTable.webhookSecret
    var referralCode by UserTable.referralCode
    var premiumId by UserTable.premiumId
    var premium by PremiumEntity optionalReferencedOn UserTable.premiumId
    var premiumAdminId by UserTable.premiumAdminId
    var premiumAdmin by PremiumEntity optionalReferencedOn UserTable.premiumAdminId
    val apiKeys by ApiKeyEntity referrersOn ApiKeyTable.userId
    val emailAccounts by EmailAccountEntity referrersOn EmailAccountTable.userId
    val referralsMade by ReferralEntity referrersOn ReferralTable.referrerUserId
    val referralReceived by ReferralEntity optionalBackReferencedOn ReferralTable.referredUserId
    val mobilePushTokens by MobilePushTokenEntity referrersOn MobilePushTokenTable.userId
    val oauthClientRecords by OauthClientEntity optionalReferrersOn OauthClientTable.userId
    val oauthRefreshTokenRecords by OauthRefreshTokenEntity referrersOn OauthRefreshTokenTable.userId
    val oauthAccessTokenRecords by OauthAccessTokenEntity optionalReferrersOn OauthAccessTokenTable.userId
    val oauthConsentRecords by OauthConsentEntity optionalReferrersOn OauthConsentTable.userId
    val ssoProviders by SsoProviderEntity optionalReferrersOn SsoProviderTable.userId
    var scimAccessDisabled by UserTable.scimAccessDisabled
    val scimIdentityTombstones by ScimIdentityTombstoneEntity referrersOn ScimIdentityTombstoneTable.userId
    val scimSubject by ScimSubjectEntity optionalBackReferencedOn ScimSubjectTable.userId
    val scimUsers by ScimUserEntity referrersOn ScimUserTable.userId
    val scimProjectionGrants by ScimProjectionGrantEntity referrersOn ScimProjectionGrantTable.userId
    val scimIdentityLinks by ScimIdentityLinkEntity referrersOn ScimIdentityLinkTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
