package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AuthzEngineSwitchEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AuthzEngineSwitchEntity>(AuthzEngineSwitchTable)

    var onEngine by AuthzEngineSwitchTable.onEngine
    var pinned by AuthzEngineSwitchTable.pinned
    var switchedAt by AuthzEngineSwitchTable.switchedAt
    var lastAppliedEventId by AuthzEngineSwitchTable.lastAppliedEventId
    var updatedAt by AuthzEngineSwitchTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AuthzEngineSwitchTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
