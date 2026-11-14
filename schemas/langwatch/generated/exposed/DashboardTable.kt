package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DashboardTable : IdTable<String>("\"Dashboard\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val order = integer("order").default(0)
    val description = text("description").nullable()
    val createdById = text("createdById").nullable()
    val scope = pgEnum("scope", "\"DashboardScope\"", DashboardScope.entries, DashboardScope::dbName).default(DashboardScope.PROJECT)
    val organizationId = text("organizationId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Dashboard_pkey\"")

    init {
        index("\"Dashboard_projectId_idx\"", false, projectId)
        index("\"Dashboard_projectId_order_idx\"", false, projectId, order)
        index("\"Dashboard_organizationId_scope_idx\"", false, organizationId, scope)
    }
}
