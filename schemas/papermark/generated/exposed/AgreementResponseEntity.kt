package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AgreementResponseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AgreementResponseEntity>(AgreementResponseTable)

    var agreementId by AgreementResponseTable.agreementId
    var agreement by AgreementEntity referencedOn AgreementResponseTable.agreementId
    var viewId by AgreementResponseTable.viewId
    var view by ViewEntity optionalReferencedOn AgreementResponseTable.viewId
    var linkId by AgreementResponseTable.linkId
    var signerEmail by AgreementResponseTable.signerEmail
    var signerName by AgreementResponseTable.signerName
    var signingStatus by AgreementResponseTable.signingStatus
    var signingExternalId by AgreementResponseTable.signingExternalId
    var signingEnvelopeId by AgreementResponseTable.signingEnvelopeId
    var signingDocumentId by AgreementResponseTable.signingDocumentId
    var signedAt by AgreementResponseTable.signedAt
    var completedAt by AgreementResponseTable.completedAt
    var signedFileKey by AgreementResponseTable.signedFileKey
    var signedFileName by AgreementResponseTable.signedFileName
    var signedFileStorageType by AgreementResponseTable.signedFileStorageType
    var createdAt by AgreementResponseTable.createdAt
    var updatedAt by AgreementResponseTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AgreementResponseTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
