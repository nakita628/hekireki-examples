package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ModelProviderTable : IdTable<String>("\"ModelProvider\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name")
    val provider = text("provider")
    val routingHandle = text("routingHandle").nullable()
    val enabled = bool("enabled")
    val customKeys = jsonb<String>("customKeys", { it }, { it }).nullable()
    val extraHeaders = jsonb<String>("extraHeaders", { it }, { it }).nullable()
    val customModels = jsonb<String>("customModels", { it }, { it }).nullable()
    val customEmbeddingsModels = jsonb<String>("customEmbeddingsModels", { it }, { it }).nullable()
    val deploymentMapping = jsonb<String>("deploymentMapping", { it }, { it }).nullable()
    val rateLimitRpm = integer("rateLimitRpm").nullable()
    val rateLimitTpm = integer("rateLimitTpm").nullable()
    val rateLimitRpd = integer("rateLimitRpd").nullable()
    val rotationPolicy =
        pgEnum(
            "rotationPolicy",
            "\"GatewayProviderRotationPolicy\"",
            GatewayProviderRotationPolicy.entries,
            GatewayProviderRotationPolicy::dbName,
        ).default(GatewayProviderRotationPolicy.MANUAL)
    val providerConfig = jsonb<String>("providerConfig", { it }, { it }).nullable()
    val fallbackPriorityGlobal = integer("fallbackPriorityGlobal").nullable()
    val langySkipPermissionsModels = jsonb<String>("langySkipPermissionsModels", { it }, { it }).nullable()
    val healthStatus =
        pgEnum("healthStatus", "\"GatewayProviderHealthStatus\"", GatewayProviderHealthStatus.entries, GatewayProviderHealthStatus::dbName)
            .default(GatewayProviderHealthStatus.UNKNOWN)
    val circuitOpenedAt = pgTimestamp("circuitOpenedAt", 3).nullable()
    val lastHealthCheckAt = pgTimestamp("lastHealthCheckAt", 3).nullable()
    val disabledAt = pgTimestamp("disabledAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organizationId = text("organizationId")
    override val primaryKey = PrimaryKey(id, name = "\"ModelProvider_pkey\"")

    init {
        index("\"ModelProvider_healthStatus_idx\"", false, healthStatus)
        index("\"ModelProvider_fallbackPriorityGlobal_idx\"", false, fallbackPriorityGlobal)
        index("\"ModelProvider_organizationId_idx\"", false, organizationId)
        uniqueIndex("\"ModelProvider_organizationId_routingHandle_key\"", organizationId, routingHandle)
    }
}
