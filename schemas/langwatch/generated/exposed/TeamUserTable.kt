package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TeamUserTable : CompositeIdTable("\"TeamUser\"") {
    val userId = text("userId").entityId()
    val teamId = text("teamId").entityId()
    val role = pgEnum("role", "\"TeamUserRole\"", TeamUserRole.entries, TeamUserRole::dbName)
    val assignedRoleId = text("assignedRoleId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(userId, teamId, name = "\"TeamUser_pkey\"")

    init {
        index("\"TeamUser_teamId_idx\"", false, teamId)
        index("\"TeamUser_userId_idx\"", false, userId)
        index("\"TeamUser_assignedRoleId_idx\"", false, assignedRoleId)
    }
}
