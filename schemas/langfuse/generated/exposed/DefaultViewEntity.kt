package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DefaultViewEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DefaultViewEntity>(DefaultViewTable)

    var createdAt by DefaultViewTable.createdAt
    var updatedAt by DefaultViewTable.updatedAt
    var projectId by DefaultViewTable.projectId
    var project by ProjectEntity referencedOn DefaultViewTable.projectId
    var userId by DefaultViewTable.userId
    var user by UserEntity optionalReferencedOn DefaultViewTable.userId
    var viewName by DefaultViewTable.viewName
    var viewId by DefaultViewTable.viewId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DefaultViewTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
