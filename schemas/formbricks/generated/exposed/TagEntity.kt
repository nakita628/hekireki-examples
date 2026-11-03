package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TagEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TagEntity>(TagTable)

    var createdAt by TagTable.createdAt
    var updatedAt by TagTable.updatedAt
    var name by TagTable.name
    val responses by TagsOnResponsesEntity referrersOn TagsOnResponsesTable.tagId
    var workspace by WorkspaceEntity referencedOn TagTable.workspaceId
    var workspaceId by TagTable.workspaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TagTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
