package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebhookScheduledTriggersTable : IdTable<Int>("\"WebhookScheduledTriggers\"") {
    override val id = integer("id").autoIncrement().entityId()
    val jobName = text("jobName").nullable()
    val subscriberUrl = text("subscriberUrl")
    val payload = text("payload")
    val startAfter = pgTimestamp("startAfter", 3)
    val retryCount = integer("retryCount").default(0)
    val createdAt =
        pgTimestamp("createdAt", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val appId = text("appId").nullable()
    val webhookId =
        optReference(
            "webhookId",
            WebhookTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WebhookScheduledTriggers_webhookId_fkey\"",
        )
    val bookingId =
        optReference(
            "bookingId",
            BookingTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WebhookScheduledTriggers_bookingId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"WebhookScheduledTriggers_pkey\"")
}
