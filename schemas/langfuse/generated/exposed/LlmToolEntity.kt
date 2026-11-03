package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LlmToolEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LlmToolEntity>(LlmToolTable)

    var createdAt by LlmToolTable.createdAt
    var updatedAt by LlmToolTable.updatedAt
    var projectId by LlmToolTable.projectId
    var project by ProjectEntity referencedOn LlmToolTable.projectId
    var name by LlmToolTable.name
    var description by LlmToolTable.description
    var parameters by LlmToolTable.parameters

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LlmToolTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
