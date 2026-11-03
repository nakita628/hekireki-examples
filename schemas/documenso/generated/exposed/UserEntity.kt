package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, UserEntity>(UserTable)

    var name by UserTable.name
    var email by UserTable.email
    var emailVerified by UserTable.emailVerified
    var password by UserTable.password
    var source by UserTable.source1
    var signature by UserTable.signature
    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt
    var lastSignedIn by UserTable.lastSignedIn
    var roles by UserTable.roles
    var identityProvider by UserTable.identityProvider
    var avatarImageId by UserTable.avatarImageId
    var disabled by UserTable.disabled
    val accounts by AccountEntity referrersOn AccountTable.userId
    val sessions by SessionEntity referrersOn SessionTable.userId
    val passwordResetTokens by PasswordResetTokenEntity referrersOn PasswordResetTokenTable.userId
    val ownedOrganisations by OrganisationEntity referrersOn OrganisationTable.ownerUserId
    val organisationMember by OrganisationMemberEntity referrersOn OrganisationMemberTable.userId
    var twoFactorSecret by UserTable.twoFactorSecret
    var twoFactorEnabled by UserTable.twoFactorEnabled
    var twoFactorBackupCodes by UserTable.twoFactorBackupCodes
    val folders by FolderEntity referrersOn FolderTable.userId
    val envelopes by EnvelopeEntity referrersOn EnvelopeTable.userId
    val verificationTokens by VerificationTokenEntity referrersOn VerificationTokenTable.userId
    val apiTokens by ApiTokenEntity optionalReferrersOn ApiTokenTable.userId
    val securityAuditLogs by UserSecurityAuditLogEntity referrersOn UserSecurityAuditLogTable.userId
    val webhooks by WebhookEntity referrersOn WebhookTable.userId
    val siteSettings by SiteSettingsEntity optionalReferrersOn SiteSettingsTable.lastModifiedByUserId
    val passkeys by PasskeyEntity referrersOn PasskeyTable.userId
    var avatarImage by AvatarImageEntity optionalReferencedOn UserTable.avatarImageId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
