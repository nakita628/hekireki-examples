package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AiToolEntryTable : IdTable<String>("\"AiToolEntry\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val scope = text("scope")
    val scopeId = text("scopeId")
    val type = text("type")
    val displayName = text("displayName")
    val slug = text("slug")
    val iconKey = text("iconKey").nullable()
    val iconAsset = text("iconAsset").nullable()
    val order = integer("order").default(0)
    val enabled = bool("enabled").default(true)
    val config = jsonb<String>("config", { it }, { it }).default("{}")
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"AiToolEntry_pkey\"")

    init {
        index("\"AiToolEntry_organizationId_scope_scopeId_idx\"", false, organizationId, scope, scopeId)
        index("\"AiToolEntry_organizationId_enabled_archivedAt_idx\"", false, organizationId, enabled, archivedAt)
        index("\"AiToolEntry_organizationId_type_idx\"", false, organizationId, type)
    }
}
