package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class VerificationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, VerificationEntity>(VerificationTable)

    var identifier by VerificationTable.identifier
    var value by VerificationTable.value
    var expiresAt by VerificationTable.expiresAt
    var createdAt by VerificationTable.createdAt
    var updatedAt by VerificationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(VerificationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
