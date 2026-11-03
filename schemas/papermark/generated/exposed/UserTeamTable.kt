package papermark.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb

object UserTeamTable : CompositeIdTable("\"UserTeam\"") {
    val role = pgEnum("role", "\"Role\"", Role.entries, Role::dbName).default(Role.MEMBER)
    val status = text("status").default("ACTIVE")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserTeam_userId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserTeam_teamId_fkey\"",
        )
    val blockedAt = pgTimestamp("blockedAt", 3).nullable()
    val notificationPreferences = jsonb<String>("notificationPreferences", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(userId, teamId, name = "\"UserTeam_pkey\"")

    init {
        addIdColumn(userId)
        addIdColumn(teamId)
        index("\"UserTeam_userId_idx\"", false, userId)
        index("\"UserTeam_teamId_idx\"", false, teamId)
    }
}
