package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TableViewPresetEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TableViewPresetEntity>(TableViewPresetTable)

    var createdAt by TableViewPresetTable.createdAt
    var updatedAt by TableViewPresetTable.updatedAt
    var projectId by TableViewPresetTable.projectId
    var project by ProjectEntity referencedOn TableViewPresetTable.projectId
    var name by TableViewPresetTable.name
    var tableName by TableViewPresetTable.tableName1
    var createdBy by TableViewPresetTable.createdBy
    var createdByUser by UserEntity optionalReferencedOn TableViewPresetTable.createdBy
    var updatedBy by TableViewPresetTable.updatedBy
    var updatedByUser by UserEntity optionalReferencedOn TableViewPresetTable.updatedBy
    var filters by TableViewPresetTable.filters
    var columnOrder by TableViewPresetTable.columnOrder
    var columnVisibility by TableViewPresetTable.columnVisibility
    var searchQuery by TableViewPresetTable.searchQuery
    var orderBy by TableViewPresetTable.orderBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TableViewPresetTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
