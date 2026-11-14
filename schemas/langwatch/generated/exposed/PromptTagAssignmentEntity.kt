package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PromptTagAssignmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PromptTagAssignmentEntity>(PromptTagAssignmentTable)

    var configId by PromptTagAssignmentTable.configId
    var versionId by PromptTagAssignmentTable.versionId
    var tagId by PromptTagAssignmentTable.tagId
    var projectId by PromptTagAssignmentTable.projectId
    var createdAt by PromptTagAssignmentTable.createdAt
    var updatedAt by PromptTagAssignmentTable.updatedAt
    var createdById by PromptTagAssignmentTable.createdById
    var updatedById by PromptTagAssignmentTable.updatedById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PromptTagAssignmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
