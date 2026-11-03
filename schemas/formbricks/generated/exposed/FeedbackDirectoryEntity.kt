package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FeedbackDirectoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FeedbackDirectoryEntity>(FeedbackDirectoryTable)

    var createdAt by FeedbackDirectoryTable.createdAt
    var updatedAt by FeedbackDirectoryTable.updatedAt
    var name by FeedbackDirectoryTable.name
    var isArchived by FeedbackDirectoryTable.isArchived
    var organizationId by FeedbackDirectoryTable.organizationId
    var organization by OrganizationEntity referencedOn FeedbackDirectoryTable.organizationId
    val workspaces by FeedbackDirectoryWorkspaceEntity referrersOn FeedbackDirectoryWorkspaceTable.feedbackDirectoryId
    val feedbackSources by FeedbackSourceEntity referrersOn FeedbackSourceTable.feedbackDirectoryId
    val charts by ChartEntity referrersOn ChartTable.feedbackDirectoryId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FeedbackDirectoryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
