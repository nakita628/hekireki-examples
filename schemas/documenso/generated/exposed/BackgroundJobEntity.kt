package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BackgroundJobEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BackgroundJobEntity>(BackgroundJobTable)

    var status by BackgroundJobTable.status
    var payload by BackgroundJobTable.payload
    var retried by BackgroundJobTable.retried
    var maxRetries by BackgroundJobTable.maxRetries
    var jobId by BackgroundJobTable.jobId
    var name by BackgroundJobTable.name
    var version by BackgroundJobTable.version
    var submittedAt by BackgroundJobTable.submittedAt
    var updatedAt by BackgroundJobTable.updatedAt
    var completedAt by BackgroundJobTable.completedAt
    var lastRetriedAt by BackgroundJobTable.lastRetriedAt
    val tasks by BackgroundJobTaskEntity referrersOn BackgroundJobTaskTable.jobId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BackgroundJobTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
