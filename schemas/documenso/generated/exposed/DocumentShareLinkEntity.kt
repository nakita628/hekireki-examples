package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DocumentShareLinkEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, DocumentShareLinkEntity>(DocumentShareLinkTable)

    var email by DocumentShareLinkTable.email
    var slug by DocumentShareLinkTable.slug
    var envelopeId by DocumentShareLinkTable.envelopeId
    var createdAt by DocumentShareLinkTable.createdAt
    var updatedAt by DocumentShareLinkTable.updatedAt
    var envelope by EnvelopeEntity referencedOn DocumentShareLinkTable.envelopeId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DocumentShareLinkTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
