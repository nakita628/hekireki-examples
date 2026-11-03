package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DocumentMetaEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DocumentMetaEntity>(DocumentMetaTable)

    var subject by DocumentMetaTable.subject
    var message by DocumentMetaTable.message
    var timezone by DocumentMetaTable.timezone
    var dateFormat by DocumentMetaTable.dateFormat
    var redirectUrl by DocumentMetaTable.redirectUrl
    var signingOrder by DocumentMetaTable.signingOrder
    var allowDictateNextSigner by DocumentMetaTable.allowDictateNextSigner
    var typedSignatureEnabled by DocumentMetaTable.typedSignatureEnabled
    var uploadSignatureEnabled by DocumentMetaTable.uploadSignatureEnabled
    var drawSignatureEnabled by DocumentMetaTable.drawSignatureEnabled
    var language by DocumentMetaTable.language
    var distributionMethod by DocumentMetaTable.distributionMethod
    var emailSettings by DocumentMetaTable.emailSettings
    var emailReplyTo by DocumentMetaTable.emailReplyTo
    var emailId by DocumentMetaTable.emailId
    var envelopeExpirationPeriod by DocumentMetaTable.envelopeExpirationPeriod
    var reminderSettings by DocumentMetaTable.reminderSettings
    val envelope by EnvelopeEntity optionalBackReferencedOn EnvelopeTable.documentMetaId
}
