package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PluginContextMenuEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PluginContextMenuEntity>(PluginContextMenuTable)

    var tableId by PluginContextMenuTable.tableId
    var pluginInstallId by PluginContextMenuTable.pluginInstallId
    var order by PluginContextMenuTable.order
    var createdTime by PluginContextMenuTable.createdTime
    var createdBy by PluginContextMenuTable.createdBy
    var lastModifiedTime by PluginContextMenuTable.lastModifiedTime
    var lastModifiedBy by PluginContextMenuTable.lastModifiedBy
    var table by TableMetaEntity referencedOn PluginContextMenuTable.tableId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PluginContextMenuTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
