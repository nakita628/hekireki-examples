package formbricks.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TeamUserTable : CompositeIdTable("\"TeamUser\"") {
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamUser_teamId_fkey\"",
        )
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamUser_userId_fkey\"",
        )
    val role = pgEnum("role", "\"TeamUserRole\"", TeamUserRole.entries, TeamUserRole::dbName)
    override val primaryKey = PrimaryKey(teamId, userId, name = "\"TeamUser_pkey\"")

    init {
        addIdColumn(teamId)
        addIdColumn(userId)
        index("\"TeamUser_userId_idx\"", false, userId)
    }
}
