package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BaseNodeFolderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BaseNodeFolderEntity>(BaseNodeFolderTable)

    var baseId by BaseNodeFolderTable.baseId
    var name by BaseNodeFolderTable.name
    var createdTime by BaseNodeFolderTable.createdTime
    var createdBy by BaseNodeFolderTable.createdBy
    var lastModifiedTime by BaseNodeFolderTable.lastModifiedTime
    var lastModifiedBy by BaseNodeFolderTable.lastModifiedBy
    var deletedTime by BaseNodeFolderTable.deletedTime

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BaseNodeFolderTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
