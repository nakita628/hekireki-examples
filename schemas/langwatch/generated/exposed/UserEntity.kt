package langwatch.models

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
    var signupConfirmationPending by UserTable.signupConfirmationPending
    var passkeySignupClaimHash by UserTable.passkeySignupClaimHash
    var image by UserTable.image
    var pendingSsoSetup by UserTable.pendingSsoSetup
    var userHashKey by UserTable.userHashKey
    var twoFactorEnabled by UserTable.twoFactorEnabled
    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt
    var lastLoginAt by UserTable.lastLoginAt
    var deactivatedAt by UserTable.deactivatedAt
    var lastHomePath by UserTable.lastHomePath
    var tracesExplorerTourDismissedAt by UserTable.tracesExplorerTourDismissedAt
    var langyCodeAccessPreference by UserTable.langyCodeAccessPreference
    var passkeyNudgeDismissedAt by UserTable.passkeyNudgeDismissedAt
    var joinOfferDismissedDomains by UserTable.joinOfferDismissedDomains
    var notificationPreferences by UserTable.notificationPreferences
    var passwordResetRequiredAt by UserTable.passwordResetRequiredAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
