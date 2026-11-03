package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LlmSchemaEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LlmSchemaEntity>(LlmSchemaTable)

    var createdAt by LlmSchemaTable.createdAt
    var updatedAt by LlmSchemaTable.updatedAt
    var projectId by LlmSchemaTable.projectId
    var project by ProjectEntity referencedOn LlmSchemaTable.projectId
    var name by LlmSchemaTable.name
    var description by LlmSchemaTable.description
    var schema by LlmSchemaTable.schema

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LlmSchemaTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
