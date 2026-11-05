package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class VerificationTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, VerificationTokenEntity>(VerificationTokenTable)

    var createdAt by VerificationTokenTable.createdAt
    var updatedAt by VerificationTokenTable.updatedAt
    var identifier by VerificationTokenTable.identifier
    var token by VerificationTokenTable.token
    var expires by VerificationTokenTable.expires

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(VerificationTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
