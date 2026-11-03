package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class InfraConfigEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InfraConfigEntity>(InfraConfigTable)

    var name by InfraConfigTable.name
    var value by InfraConfigTable.value
    var isEncrypted by InfraConfigTable.isEncrypted
    var lastSyncedEnvFileValue by InfraConfigTable.lastSyncedEnvFileValue
    var createdOn by InfraConfigTable.createdOn
    var updatedOn by InfraConfigTable.updatedOn

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(InfraConfigTable.updatedOn)) {
                updatedOn = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
