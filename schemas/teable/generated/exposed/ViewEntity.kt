package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ViewEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ViewEntity>(ViewTable)

    var name by ViewTable.name
    var description by ViewTable.description
    var tableId by ViewTable.tableId
    var type by ViewTable.type
    var sort by ViewTable.sort
    var filter by ViewTable.filter
    var group by ViewTable.group
    var options by ViewTable.options1
    var order by ViewTable.order
    var version by ViewTable.version
    var columnMeta by ViewTable.columnMeta
    var isLocked by ViewTable.isLocked
    var enableShare by ViewTable.enableShare
    var shareId by ViewTable.shareId
    var shareMeta by ViewTable.shareMeta
    var createdTime by ViewTable.createdTime
    var lastModifiedTime by ViewTable.lastModifiedTime
    var deletedTime by ViewTable.deletedTime
    var createdBy by ViewTable.createdBy
    var lastModifiedBy by ViewTable.lastModifiedBy
    var table by TableMetaEntity referencedOn ViewTable.tableId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ViewTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
