package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class VerificationTokenEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, VerificationTokenEntity>(VerificationTokenTable)

    var identifier by VerificationTokenTable.identifier
    var token by VerificationTokenTable.token
    var expires by VerificationTokenTable.expires
    var expiresInDays by VerificationTokenTable.expiresInDays
    var createdAt by VerificationTokenTable.createdAt
    var updatedAt by VerificationTokenTable.updatedAt
    var teamId by VerificationTokenTable.teamId
    var team by TeamEntity optionalReferencedOn VerificationTokenTable.teamId
    var secondaryEmailId by VerificationTokenTable.secondaryEmailId
    var secondaryEmail by SecondaryEmailEntity optionalReferencedOn VerificationTokenTable.secondaryEmailId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(VerificationTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
