package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MobilePushDeliveryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MobilePushDeliveryEntity>(MobilePushDeliveryTable)

    var createdAt by MobilePushDeliveryTable.createdAt
    var deduplicationKey by MobilePushDeliveryTable.deduplicationKey
    var mobilePushTokenId by MobilePushDeliveryTable.mobilePushTokenId
    var mobilePushToken by MobilePushTokenEntity referencedOn MobilePushDeliveryTable.mobilePushTokenId
}
