package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class AlertDeliveryEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<AlertDeliveryEntity>(AlertDeliveryTable)

    val id1 by AlertDeliveryTable.id1
    var alertEventId by AlertDeliveryTable.alertEventId
    val eventAt by AlertDeliveryTable.eventAt
    var notifierId by AlertDeliveryTable.notifierId
    var notifier by AlertDeliveryTable.notifier
    var status by AlertDeliveryTable.status
    var claimId by AlertDeliveryTable.claimId
    var claimedAt by AlertDeliveryTable.claimedAt
    var deliveredAt by AlertDeliveryTable.deliveredAt
    var failureReason by AlertDeliveryTable.failureReason
    var providerMetadata by AlertDeliveryTable.providerMetadata
    var attempts by AlertDeliveryTable.attempts
    var firstAttemptAt by AlertDeliveryTable.firstAttemptAt
    var createdAt by AlertDeliveryTable.createdAt
}
