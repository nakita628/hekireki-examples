package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CscCredentialEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CscCredentialEntity>(CscCredentialTable)

    var providerId by CscCredentialTable.providerId
    var credentialId by CscCredentialTable.credentialId
    var certCache by CscCredentialTable.certCache
    var signatureAlgorithm by CscCredentialTable.signatureAlgorithm
    var keyType by CscCredentialTable.keyType
    var digestAlgorithm by CscCredentialTable.digestAlgorithm
    var keyLenBits by CscCredentialTable.keyLenBits
    var signAlgoParams by CscCredentialTable.signAlgoParams
    var serviceTokenCiphertext by CscCredentialTable.serviceTokenCiphertext
    var serviceTokenExpiresAt by CscCredentialTable.serviceTokenExpiresAt
    var recipientId by CscCredentialTable.recipientId
    var recipient by RecipientEntity referencedOn CscCredentialTable.recipientId
    var createdAt by CscCredentialTable.createdAt
    var updatedAt by CscCredentialTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CscCredentialTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
