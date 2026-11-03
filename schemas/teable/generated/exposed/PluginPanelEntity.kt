package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PluginPanelEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PluginPanelEntity>(PluginPanelTable)

    var name by PluginPanelTable.name
    var tableId by PluginPanelTable.tableId
    var layout by PluginPanelTable.layout
    var createdBy by PluginPanelTable.createdBy
    var createdTime by PluginPanelTable.createdTime
    var lastModifiedTime by PluginPanelTable.lastModifiedTime
    var lastModifiedBy by PluginPanelTable.lastModifiedBy
    var table by TableMetaEntity referencedOn PluginPanelTable.tableId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PluginPanelTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
