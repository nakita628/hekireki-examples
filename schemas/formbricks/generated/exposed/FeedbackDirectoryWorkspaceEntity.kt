package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class FeedbackDirectoryWorkspaceEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<FeedbackDirectoryWorkspaceEntity>(FeedbackDirectoryWorkspaceTable)

    var createdAt by FeedbackDirectoryWorkspaceTable.createdAt
    var updatedAt by FeedbackDirectoryWorkspaceTable.updatedAt
    val feedbackDirectoryId by FeedbackDirectoryWorkspaceTable.feedbackDirectoryId
    val feedbackDirectory by FeedbackDirectoryEntity referencedOn FeedbackDirectoryWorkspaceTable.feedbackDirectoryId
    val workspaceId by FeedbackDirectoryWorkspaceTable.workspaceId
    val workspace by WorkspaceEntity referencedOn FeedbackDirectoryWorkspaceTable.workspaceId
    val feedbackSources by FeedbackSourceEntity referrersOn FeedbackSourceTable

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FeedbackDirectoryWorkspaceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
