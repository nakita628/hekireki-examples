package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AiScheduledTaskEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AiScheduledTaskEntity>(AiScheduledTaskTable)

    var name by AiScheduledTaskTable.name
    var prompt by AiScheduledTaskTable.prompt
    var schedule by AiScheduledTaskTable.schedule
    var isEnabled by AiScheduledTaskTable.isEnabled
    var lastRun by AiScheduledTaskTable.lastRun
    var lastResult by AiScheduledTaskTable.lastResult
    var accountId by AiScheduledTaskTable.accountId
    var createdAt by AiScheduledTaskTable.createdAt
    var updatedAt by AiScheduledTaskTable.updatedAt
    var account by AccountsEntity referencedOn AiScheduledTaskTable.accountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AiScheduledTaskTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
