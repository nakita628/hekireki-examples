package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FeedbackSourceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FeedbackSourceEntity>(FeedbackSourceTable)

    var createdAt by FeedbackSourceTable.createdAt
    var updatedAt by FeedbackSourceTable.updatedAt
    var name by FeedbackSourceTable.name
    var type by FeedbackSourceTable.type
    var status by FeedbackSourceTable.status
    var importMode by FeedbackSourceTable.importMode
    var elementScope by FeedbackSourceTable.elementScope
    var workspaceId by FeedbackSourceTable.workspaceId
    var workspace by WorkspaceEntity referencedOn FeedbackSourceTable.workspaceId
    var feedbackDirectoryId by FeedbackSourceTable.feedbackDirectoryId
    var feedbackDirectory by FeedbackDirectoryEntity referencedOn FeedbackSourceTable.feedbackDirectoryId
    var lastSyncAt by FeedbackSourceTable.lastSyncAt
    var createdBy by FeedbackSourceTable.createdBy
    var creator by UserEntity optionalReferencedOn FeedbackSourceTable.createdBy
    var feedbackDirectoryWorkspace by FeedbackDirectoryWorkspaceEntity referencedOn FeedbackSourceTable

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FeedbackSourceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
