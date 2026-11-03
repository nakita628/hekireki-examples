package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SettingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SettingEntity>(SettingTable)

    var content by SettingTable.content
    var createdTime by SettingTable.createdTime
    var lastModifiedTime by SettingTable.lastModifiedTime
    var createdBy by SettingTable.createdBy
    var lastModifiedBy by SettingTable.lastModifiedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SettingTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
