package jitsu.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkspaceAccessTable : CompositeIdTable("\"WorkspaceAccess\"") {
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkspaceAccess_workspaceId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserProfileTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkspaceAccess_userId_fkey\"",
        )
    val role = pgEnum("role", "\"WorkspaceRole\"", WorkspaceRole.entries, WorkspaceRole::dbName).default(WorkspaceRole.OWNER)
    override val primaryKey = PrimaryKey(userId, workspaceId, name = "\"WorkspaceAccess_pkey\"")

    init {
        addIdColumn(userId)
        addIdColumn(workspaceId)
    }
}
