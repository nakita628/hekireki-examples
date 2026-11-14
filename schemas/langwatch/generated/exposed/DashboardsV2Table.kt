package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DashboardsV2Table : IdTable<String>("\"DashboardsV2\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val description = text("description").nullable()
    val createdById = text("createdById").nullable()
    val scope = pgEnum("scope", "\"DashboardScope\"", DashboardScope.entries, DashboardScope::dbName).default(DashboardScope.PROJECT)
    val organizationId = text("organizationId").nullable()
    val excludedOrigins = jsonb<String>("excludedOrigins", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DashboardsV2_pkey\"")

    init {
        index("\"DashboardsV2_projectId_idx\"", false, projectId)
        index("\"DashboardsV2_organizationId_scope_idx\"", false, organizationId, scope)
    }
}
