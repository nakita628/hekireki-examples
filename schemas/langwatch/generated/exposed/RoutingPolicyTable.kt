package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object RoutingPolicyTable : IdTable<String>("\"RoutingPolicy\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val name = text("name")
    val description = text("description").nullable()
    val modelProviderIds = jsonb<String>("modelProviderIds", { it }, { it }).default("[]")
    val modelAliases = jsonb<String>("modelAliases", { it }, { it }).default("{}")
    val defaultModel = text("defaultModel").nullable()
    val policyRules = jsonb<String>("policyRules", { it }, { it }).default("{}")
    val isDefault = bool("isDefault").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"RoutingPolicy_pkey\"")

    init {
        index("\"RoutingPolicy_organizationId_idx\"", false, organizationId)
    }
}
