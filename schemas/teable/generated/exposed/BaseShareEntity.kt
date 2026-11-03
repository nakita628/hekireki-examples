package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BaseShareEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BaseShareEntity>(BaseShareTable)

    var baseId by BaseShareTable.baseId
    var shareId by BaseShareTable.shareId
    var password by BaseShareTable.password
    var nodeId by BaseShareTable.nodeId
    var allowSave by BaseShareTable.allowSave
    var allowCopy by BaseShareTable.allowCopy
    var allowEdit by BaseShareTable.allowEdit
    var enabled by BaseShareTable.enabled
    var createdTime by BaseShareTable.createdTime
    var createdBy by BaseShareTable.createdBy
    var lastModifiedTime by BaseShareTable.lastModifiedTime

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BaseShareTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
