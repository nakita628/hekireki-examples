package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LlmPromptConfigEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LlmPromptConfigEntity>(LlmPromptConfigTable)

    var handle by LlmPromptConfigTable.handle
    var name by LlmPromptConfigTable.name
    var projectId by LlmPromptConfigTable.projectId
    var createdAt by LlmPromptConfigTable.createdAt
    var updatedAt by LlmPromptConfigTable.updatedAt
    var deletedAt by LlmPromptConfigTable.deletedAt
    var organizationId by LlmPromptConfigTable.organizationId
    var scope by LlmPromptConfigTable.scope
    var copiedFromPromptId by LlmPromptConfigTable.copiedFromPromptId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LlmPromptConfigTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
