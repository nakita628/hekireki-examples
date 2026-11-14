package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebhookEndpointTable : IdTable<String>("\"WebhookEndpoint\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val destinationKind =
        pgEnum("destinationKind", "\"WebhookDestinationKind\"", WebhookDestinationKind.entries, WebhookDestinationKind::dbName).default(
            WebhookDestinationKind.HTTP,
        )
    val url = text("url").nullable()
    val sqsQueueUrl = text("sqsQueueUrl").nullable()
    val sqsRoleArn = text("sqsRoleArn").nullable()
    val sqsExternalId = text("sqsExternalId").nullable()
    val sqsAccessKeyId = text("sqsAccessKeyId").nullable()
    val sqsSecretAccessKeyEncrypted = text("sqsSecretAccessKeyEncrypted").nullable()
    val secretEncrypted = text("secretEncrypted")
    val signatureScheme = text("signatureScheme").nullable()
    val idempotencyKey = text("idempotencyKey").nullable()
    val previousSecretEncrypted = text("previousSecretEncrypted").nullable()
    val previousSecretExpiresAt = pgTimestamp("previousSecretExpiresAt", 3).nullable()
    val enabledEvents = array("enabledEvents", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val status =
        pgEnum("status", "\"WebhookEndpointStatus\"", WebhookEndpointStatus.entries, WebhookEndpointStatus::dbName).default(
            WebhookEndpointStatus.ACTIVE,
        )
    val disabledReason = text("disabledReason").nullable()
    val disabledAt = pgTimestamp("disabledAt", 3).nullable()
    val failingSince = pgTimestamp("failingSince", 3).nullable()
    val maxBatchSize = integer("maxBatchSize").default(100)
    val maxBatchDelayMs = integer("maxBatchDelayMs").default(250)
    val maxInFlight = integer("maxInFlight").default(4)
    val allowSelfSignedCertificate = bool("allowSelfSignedCertificate").default(false)
    val lastSuccessAt = pgTimestamp("lastSuccessAt", 3).nullable()
    val lastFailureAt = pgTimestamp("lastFailureAt", 3).nullable()
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"WebhookEndpoint_pkey\"")

    init {
        index("\"WebhookEndpoint_organizationId_status_idx\"", false, organizationId, status)
        uniqueIndex("\"WebhookEndpoint_organizationId_idempotencyKey_key\"", organizationId, idempotencyKey)
    }
}
