package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RegisteredInstanceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RegisteredInstanceEntity>(RegisteredInstanceTable)

    var instanceId by RegisteredInstanceTable.instanceId
    var version by RegisteredInstanceTable.version
    var firstSeenAt by RegisteredInstanceTable.firstSeenAt
    var lastSeenAt by RegisteredInstanceTable.lastSeenAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RegisteredInstanceTable.lastSeenAt)) {
                lastSeenAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
