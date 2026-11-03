package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PromptProtectedLabelsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PromptProtectedLabelsEntity>(PromptProtectedLabelsTable)

    var createdAt by PromptProtectedLabelsTable.createdAt
    var updatedAt by PromptProtectedLabelsTable.updatedAt
    var projectId by PromptProtectedLabelsTable.projectId
    var project by ProjectEntity referencedOn PromptProtectedLabelsTable.projectId
    var label by PromptProtectedLabelsTable.label

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PromptProtectedLabelsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
