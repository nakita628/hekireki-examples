package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TwoFactorOtpUsedEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TwoFactorOtpUsedEntity>(TwoFactorOtpUsedTable)

    var userId by TwoFactorOtpUsedTable.userId
    var otp by TwoFactorOtpUsedTable.otp
    var expiresAt by TwoFactorOtpUsedTable.expiresAt
}
