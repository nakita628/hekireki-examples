package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PluginEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PluginEntity>(PluginTable)

    var name by PluginTable.name
    var description by PluginTable.description
    var detailDesc by PluginTable.detailDesc
    var logo by PluginTable.logo
    var helpUrl by PluginTable.helpUrl
    var status by PluginTable.status
    var positions by PluginTable.positions
    var url by PluginTable.url
    var secret by PluginTable.secret
    var maskedSecret by PluginTable.maskedSecret
    var i18n by PluginTable.i18n
    var config by PluginTable.config
    var pluginUser by PluginTable.pluginUser
    var createdTime by PluginTable.createdTime
    var lastModifiedTime by PluginTable.lastModifiedTime
    var createdBy by PluginTable.createdBy
    var lastModifiedBy by PluginTable.lastModifiedBy
    val pluginInstall by PluginInstallEntity referrersOn PluginInstallTable.pluginId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PluginTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
