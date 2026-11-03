package formbricks.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FeedbackDirectoryWorkspaceTable : CompositeIdTable("\"FeedbackDirectoryWorkspace\"") {
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val feedbackDirectoryId =
        reference(
            "feedbackDirectoryId",
            FeedbackDirectoryTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"FeedbackDirectoryWorkspace_feedbackDirectoryId_fkey\"",
        )
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"FeedbackDirectoryWorkspace_workspaceId_fkey\"",
        )
    override val primaryKey = PrimaryKey(feedbackDirectoryId, workspaceId, name = "\"FeedbackDirectoryWorkspace_pkey\"")

    init {
        addIdColumn(feedbackDirectoryId)
        addIdColumn(workspaceId)
        index("\"FeedbackDirectoryWorkspace_workspaceId_idx\"", false, workspaceId)
    }
}
