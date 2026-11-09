package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GlobalPropsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GlobalPropsEntity>(GlobalPropsTable)

    var createdAt by GlobalPropsTable.createdAt
    var updatedAt by GlobalPropsTable.updatedAt
    var name by GlobalPropsTable.name
    var value by GlobalPropsTable.value

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GlobalPropsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
