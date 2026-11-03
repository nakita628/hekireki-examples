package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AgreementEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AgreementEntity>(AgreementTable)

    var name by AgreementTable.name
    var content by AgreementTable.content
    var contentType by AgreementTable.contentType
    var signingProvider by AgreementTable.signingProvider
    var signingExternalId by AgreementTable.signingExternalId
    var signingEnvelopeId by AgreementTable.signingEnvelopeId
    var signingTemplateId by AgreementTable.signingTemplateId
    val links by LinkEntity optionalReferrersOn LinkTable.agreementId
    val responses by AgreementResponseEntity referrersOn AgreementResponseTable.agreementId
    var requireName by AgreementTable.requireName
    var teamId by AgreementTable.teamId
    var team by TeamEntity referencedOn AgreementTable.teamId
    var createdAt by AgreementTable.createdAt
    var updatedAt by AgreementTable.updatedAt
    var deletedAt by AgreementTable.deletedAt
    var deletedBy by AgreementTable.deletedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AgreementTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
