package jitsu.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkspaceUserPropertiesTable : CompositeIdTable("\"WorkspaceUserProperties\"") {
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkspaceUserProperties_workspaceId_fkey\"",
        )
    val userId =
        reference(
            "userId",
            UserProfileTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkspaceUserProperties_userId_fkey\"",
        )
    val lastUsed = pgTimestamp("lastUsed", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(workspaceId, userId, name = "\"WorkspaceUserProperties_pkey\"")

    init {
        addIdColumn(workspaceId)
        addIdColumn(userId)
    }
}
