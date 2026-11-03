package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EmbedSignatureEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmbedSignatureEntity>(EmbedSignatureTable)

    var createdAt by EmbedSignatureTable.createdAt
    var updatedAt by EmbedSignatureTable.updatedAt
    var organisationId by EmbedSignatureTable.organisationId
    var organisation by OrganisationEntity referencedOn EmbedSignatureTable.organisationId
    var recipientId by EmbedSignatureTable.recipientId
    var signature by EmbedSignatureTable.signature

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EmbedSignatureTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
