package formbricks.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkspaceTeamTable : CompositeIdTable("\"WorkspaceTeam\"") {
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkspaceTeam_workspaceId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkspaceTeam_teamId_fkey\"",
        )
    val permission =
        pgEnum("permission", "\"WorkspaceTeamPermission\"", WorkspaceTeamPermission.entries, WorkspaceTeamPermission::dbName).default(
            WorkspaceTeamPermission.READ,
        )
    override val primaryKey = PrimaryKey(workspaceId, teamId, name = "\"WorkspaceTeam_pkey\"")

    init {
        addIdColumn(workspaceId)
        addIdColumn(teamId)
        index("\"WorkspaceTeam_teamId_idx\"", false, teamId)
    }
}
