package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebhookDeliveryTable : CompositeIdTable("\"WebhookDelivery\"") {
    val id1 = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId")
    val webhookEndpointId = text("webhookEndpointId")
    val organizationId = text("organizationId")
    val projectId = text("projectId")
    val runtimeEnvironmentId = text("runtimeEnvironmentId")
    val environmentType =
        pgEnum("environmentType", "\"RuntimeEnvironmentType\"", RuntimeEnvironmentType.entries, RuntimeEnvironmentType::dbName)
    val externalDeliveryId = text("externalDeliveryId")
    val idempotencyKey = text("idempotencyKey")
    val runId = text("runId").nullable()
    val status =
        pgEnum("status", "\"WebhookDeliveryStatus\"", WebhookDeliveryStatus.entries, WebhookDeliveryStatus::dbName).default(
            WebhookDeliveryStatus.PENDING,
        )
    val isTest = bool("isTest").default(false)
    val parsedEvent = jsonb<String>("parsedEvent", { it }, { it }).nullable()
    val headers = jsonb<String>("headers", { it }, { it }).nullable()
    val rawBodyHash = text("rawBodyHash").nullable()
    val errorMessage = text("errorMessage").nullable()
    val filterReason = text("filterReason").nullable()
    val targets = jsonb<String>("targets", { it }, { it }).default("[]")
    val createdAt =
        pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }.entityId()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val processedAt = pgTimestamp("processedAt", 3).nullable()
    val retentionDays = integer("retentionDays").default(30).entityId()
    override val primaryKey = PrimaryKey(id1, createdAt, retentionDays, name = "\"WebhookDelivery_pkey\"")

    init {
        index(
            "\"WebhookDelivery_webhookEndpointId_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"webhookEndpointId\""), indexExpression("\"createdAt\" DESC")),
        )
    }
}
