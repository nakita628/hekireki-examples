package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MobilePushTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MobilePushTokenEntity>(MobilePushTokenTable)

    var createdAt by MobilePushTokenTable.createdAt
    var updatedAt by MobilePushTokenTable.updatedAt
    var token by MobilePushTokenTable.token
    var platform by MobilePushTokenTable.platform
    var tokenType by MobilePushTokenTable.tokenType
    var appVersion by MobilePushTokenTable.appVersion
    var userId by MobilePushTokenTable.userId
    var user by UserEntity referencedOn MobilePushTokenTable.userId
    val mobilePushDeliveries by MobilePushDeliveryEntity referrersOn MobilePushDeliveryTable.mobilePushTokenId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MobilePushTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
