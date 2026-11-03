package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class InstanceSettingsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, InstanceSettingsEntity>(InstanceSettingsTable)

    var disableUserRegistration by InstanceSettingsTable.disableUserRegistration
    var instanceId by InstanceSettingsTable.instanceId
    var appName by InstanceSettingsTable.appName
    var primaryColor by InstanceSettingsTable.primaryColor
    var primaryColorDark by InstanceSettingsTable.primaryColorDark
    var logo by InstanceSettingsTable.logo
    var logoDark by InstanceSettingsTable.logoDark
    var logoIcon by InstanceSettingsTable.logoIcon
    var hideAttribution by InstanceSettingsTable.hideAttribution
    var footerLinks by InstanceSettingsTable.footerLinks
    var createdAt by InstanceSettingsTable.createdAt
    var updatedAt by InstanceSettingsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(InstanceSettingsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
