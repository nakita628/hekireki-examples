package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FieldEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FieldEntity>(FieldTable)

    var name by FieldTable.name
    var description by FieldTable.description
    var options by FieldTable.options1
    var meta by FieldTable.meta
    var aiConfig by FieldTable.aiConfig
    var type by FieldTable.type
    var cellValueType by FieldTable.cellValueType
    var isMultipleCellValue by FieldTable.isMultipleCellValue
    var dbFieldType by FieldTable.dbFieldType
    var dbFieldName by FieldTable.dbFieldName
    var provisionState by FieldTable.provisionState
    var notNull by FieldTable.notNull
    var unique by FieldTable.unique
    var isPrimary by FieldTable.isPrimary
    var isComputed by FieldTable.isComputed
    var isLookup by FieldTable.isLookup
    var isConditionalLookup by FieldTable.isConditionalLookup
    var isPending by FieldTable.isPending
    var hasError by FieldTable.hasError
    var lookupLinkedFieldId by FieldTable.lookupLinkedFieldId
    var lookupOptions by FieldTable.lookupOptions
    var tableId by FieldTable.tableId
    var order by FieldTable.order
    var version by FieldTable.version
    var createdTime by FieldTable.createdTime
    var lastModifiedTime by FieldTable.lastModifiedTime
    var deletedTime by FieldTable.deletedTime
    var createdBy by FieldTable.createdBy
    var lastModifiedBy by FieldTable.lastModifiedBy
    var table by TableMetaEntity referencedOn FieldTable.tableId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FieldTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
