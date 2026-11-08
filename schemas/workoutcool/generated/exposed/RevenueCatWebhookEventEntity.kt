package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RevenueCatWebhookEventEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RevenueCatWebhookEventEntity>(RevenueCatWebhookEventTable)

    var eventType by RevenueCatWebhookEventTable.eventType
    var eventTimestamp by RevenueCatWebhookEventTable.eventTimestamp
    var appUserId by RevenueCatWebhookEventTable.appUserId
    var environment by RevenueCatWebhookEventTable.environment
    var productId by RevenueCatWebhookEventTable.productId
    var transactionId by RevenueCatWebhookEventTable.transactionId
    var originalTransactionId by RevenueCatWebhookEventTable.originalTransactionId
    var entitlementIds by RevenueCatWebhookEventTable.entitlementIds
    var processed by RevenueCatWebhookEventTable.processed
    var processingError by RevenueCatWebhookEventTable.processingError
    var retryCount by RevenueCatWebhookEventTable.retryCount
    var rawEventData by RevenueCatWebhookEventTable.rawEventData
    var createdAt by RevenueCatWebhookEventTable.createdAt
    var updatedAt by RevenueCatWebhookEventTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RevenueCatWebhookEventTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
