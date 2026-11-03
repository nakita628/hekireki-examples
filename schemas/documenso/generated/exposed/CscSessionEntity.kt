package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CscSessionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CscSessionEntity>(CscSessionTable)

    var envelopeId by CscSessionTable.envelopeId
    var signingTime by CscSessionTable.signingTime
    var itemsJson by CscSessionTable.itemsJson
    var encryptedSad by CscSessionTable.encryptedSad
    var sadExpiresAt by CscSessionTable.sadExpiresAt
    var createdAt by CscSessionTable.createdAt
    var recipientId by CscSessionTable.recipientId
    var recipient by RecipientEntity referencedOn CscSessionTable.recipientId
}
