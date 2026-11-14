package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GrantUsageTable : IdTable<String>("\"GrantUsage\"") {
    override val id = text("grantId").entityId()
    val organizationId = text("organizationId")
    val projectId = text("projectId")
    val viewCount = integer("viewCount").default(0)
    val lastViewedAt = pgTimestamp("lastViewedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GrantUsage_pkey\"")

    init {
        index("\"GrantUsage_organizationId_idx\"", false, organizationId)
        index("\"GrantUsage_projectId_idx\"", false, projectId)
    }
}
