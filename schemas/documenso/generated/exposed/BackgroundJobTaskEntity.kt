package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BackgroundJobTaskEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BackgroundJobTaskEntity>(BackgroundJobTaskTable)

    var name by BackgroundJobTaskTable.name
    var status by BackgroundJobTaskTable.status
    var result by BackgroundJobTaskTable.result
    var retried by BackgroundJobTaskTable.retried
    var maxRetries by BackgroundJobTaskTable.maxRetries
    var createdAt by BackgroundJobTaskTable.createdAt
    var updatedAt by BackgroundJobTaskTable.updatedAt
    var completedAt by BackgroundJobTaskTable.completedAt
    var jobId by BackgroundJobTaskTable.jobId
    var backgroundJob by BackgroundJobEntity referencedOn BackgroundJobTaskTable.jobId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BackgroundJobTaskTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
