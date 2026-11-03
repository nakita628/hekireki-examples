package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PluginInstallEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PluginInstallEntity>(PluginInstallTable)

    var pluginId by PluginInstallTable.pluginId
    var baseId by PluginInstallTable.baseId
    var name by PluginInstallTable.name
    var positionId by PluginInstallTable.positionId
    var position by PluginInstallTable.position
    var storage by PluginInstallTable.storage
    var createdTime by PluginInstallTable.createdTime
    var createdBy by PluginInstallTable.createdBy
    var lastModifiedTime by PluginInstallTable.lastModifiedTime
    var lastModifiedBy by PluginInstallTable.lastModifiedBy
    var plugin by PluginEntity referencedOn PluginInstallTable.pluginId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PluginInstallTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
