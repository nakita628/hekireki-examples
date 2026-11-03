package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EmbeddedDataEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmbeddedDataEntity>(EmbeddedDataTable)

    var createdAt by EmbeddedDataTable.createdAt
    var updatedAt by EmbeddedDataTable.updatedAt
    var key by EmbeddedDataTable.key
    var name by EmbeddedDataTable.name
    var description by EmbeddedDataTable.description
    var source by EmbeddedDataTable.source1
    var dataType by EmbeddedDataTable.dataType
    var defaultValue by EmbeddedDataTable.defaultValue
    var locked by EmbeddedDataTable.locked
    var surveyId by EmbeddedDataTable.surveyId
    var workspace by WorkspaceEntity referencedOn EmbeddedDataTable.workspaceId
    var workspaceId by EmbeddedDataTable.workspaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EmbeddedDataTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
