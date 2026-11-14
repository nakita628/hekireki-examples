package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayCacheRuleTable : IdTable<String>("\"GatewayCacheRule\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val name = text("name")
    val description = text("description").nullable()
    val priority = integer("priority").default(100)
    val enabled = bool("enabled").default(true)
    val matchers = jsonb<String>("matchers", { it }, { it })
    val action = jsonb<String>("action", { it }, { it })
    val modeEnum = pgEnum("modeEnum", "\"GatewayCacheRuleMode\"", GatewayCacheRuleMode.entries, GatewayCacheRuleMode::dbName)
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById")
    override val primaryKey = PrimaryKey(id, name = "\"GatewayCacheRule_pkey\"")

    init {
        index("\"GatewayCacheRule_organizationId_archivedAt_priority_idx\"", false, organizationId, archivedAt, priority)
        index("\"GatewayCacheRule_organizationId_enabled_priority_idx\"", false, organizationId, enabled, priority)
    }
}
