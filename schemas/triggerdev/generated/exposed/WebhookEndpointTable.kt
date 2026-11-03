package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebhookEndpointTable : IdTable<String>("\"WebhookEndpoint\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"WebhookEndpoint_friendlyId_key\"")
    val opaqueId = text("opaqueId").uniqueIndex("\"WebhookEndpoint_opaqueId_key\"")
    val organizationId = text("organizationId")
    val projectId = text("projectId")
    val runtimeEnvironmentId = text("runtimeEnvironmentId")
    val environmentType =
        pgEnum("environmentType", "\"RuntimeEnvironmentType\"", RuntimeEnvironmentType.entries, RuntimeEnvironmentType::dbName)
    val endpointTenantId = text("endpointTenantId").default("")
    val endpointExternalRef = text("endpointExternalRef").default("")
    val source1 = text("source")
    val declaredId = text("declaredId")
    val routingTargets = jsonb<String>("routingTargets", { it }, { it }).default("[]")
    val verifierArtifact = jsonb<String>("verifierArtifact", { it }, { it })
    val metadata = jsonb<String>("metadata", { it }, { it }).default("{}")
    val secretProvisioning = text("secretProvisioning").default("either")
    val setupPrompt = text("setupPrompt").nullable()
    val signingSecretKey = text("signingSecretKey").nullable()
    val status =
        pgEnum("status", "\"WebhookEndpointStatus\"", WebhookEndpointStatus.entries, WebhookEndpointStatus::dbName).default(
            WebhookEndpointStatus.ACTIVE,
        )
    val manuallyDeactivatedAt = pgTimestamp("manuallyDeactivatedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"WebhookEndpoint_pkey\"")

    init {
        index("\"WebhookEndpoint_runtimeEnvironmentId_source_idx\"", false, runtimeEnvironmentId, source1)
        index(
            "\"WebhookEndpoint_projectId_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"projectId\""), indexExpression("\"createdAt\" DESC")),
        )
        uniqueIndex(
            "WebhookEndpoint_runtimeEnvironmentId_declaredId_endpointTen_key",
            runtimeEnvironmentId,
            declaredId,
            endpointTenantId,
            endpointExternalRef,
        )
    }
}
