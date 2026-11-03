package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkerGroupTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkerGroupTokenEntity>(WorkerGroupTokenTable)

    var tokenHash by WorkerGroupTokenTable.tokenHash
    var createdAt by WorkerGroupTokenTable.createdAt
    var updatedAt by WorkerGroupTokenTable.updatedAt
    val workerGroup by WorkerInstanceGroupEntity optionalBackReferencedOn WorkerInstanceGroupTable.tokenId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkerGroupTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
