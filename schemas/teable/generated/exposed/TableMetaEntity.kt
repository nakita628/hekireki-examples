package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TableMetaEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TableMetaEntity>(TableMetaTable)

    var baseId by TableMetaTable.baseId
    var name by TableMetaTable.name
    var description by TableMetaTable.description
    var icon by TableMetaTable.icon
    var dbTableName by TableMetaTable.dbTableName
    var dbViewName by TableMetaTable.dbViewName
    var searchIndex by TableMetaTable.searchIndex
    var searchIndexPolicy by TableMetaTable.searchIndexPolicy
    var queryIndexPolicy by TableMetaTable.queryIndexPolicy
    var provisionState by TableMetaTable.provisionState
    var version by TableMetaTable.version
    var order by TableMetaTable.order
    var createdTime by TableMetaTable.createdTime
    var lastModifiedTime by TableMetaTable.lastModifiedTime
    var deletedTime by TableMetaTable.deletedTime
    var createdBy by TableMetaTable.createdBy
    var lastModifiedBy by TableMetaTable.lastModifiedBy
    var base by BaseEntity referencedOn TableMetaTable.baseId
    val fields by FieldEntity referrersOn FieldTable.tableId
    val views by ViewEntity referrersOn ViewTable.tableId
    val pluginPanel by PluginPanelEntity referrersOn PluginPanelTable.tableId
    val pluginContextMenu by PluginContextMenuEntity referrersOn PluginContextMenuTable.tableId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TableMetaTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
