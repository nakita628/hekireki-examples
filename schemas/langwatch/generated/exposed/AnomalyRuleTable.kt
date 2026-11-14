package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnomalyRuleTable : IdTable<String>("\"AnomalyRule\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val scope = text("scope")
    val scopeId = text("scopeId")
    val name = text("name")
    val description = text("description").nullable()
    val severity = text("severity")
    val ruleType = text("ruleType")
    val thresholdConfig = jsonb<String>("thresholdConfig", { it }, { it }).default("{}")
    val destinationConfig = jsonb<String>("destinationConfig", { it }, { it }).default("{}")
    val status = text("status").default("active")
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"AnomalyRule_pkey\"")

    init {
        index("\"AnomalyRule_organizationId_idx\"", false, organizationId)
        index("\"AnomalyRule_scope_scopeId_idx\"", false, scope, scopeId)
        index("\"AnomalyRule_ruleType_idx\"", false, ruleType)
        index("\"AnomalyRule_status_idx\"", false, status)
        uniqueIndex("\"AnomalyRule_organizationId_name_key\"", organizationId, name)
    }
}
