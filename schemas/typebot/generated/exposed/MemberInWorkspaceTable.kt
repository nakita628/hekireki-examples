package typebot.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MemberInWorkspaceTable : CompositeIdTable("\"MemberInWorkspace\"") {
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MemberInWorkspace_userId_fkey\"",
        )
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MemberInWorkspace_workspaceId_fkey\"",
        )
    val role = pgEnum("role", "\"WorkspaceRole\"", WorkspaceRole.entries, WorkspaceRole::dbName)

    init {
        addIdColumn(userId)
        addIdColumn(workspaceId)
        uniqueIndex("\"MemberInWorkspace_userId_workspaceId_key\"", userId, workspaceId)
    }
}
