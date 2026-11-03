package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SignatureEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, SignatureEntity>(SignatureTable)

    var created by SignatureTable.created
    var recipientId by SignatureTable.recipientId
    var fieldId by SignatureTable.fieldId
    var signatureImageAsBase64 by SignatureTable.signatureImageAsBase64
    var typedSignature by SignatureTable.typedSignature
    var recipient by RecipientEntity referencedOn SignatureTable.recipientId
    var field by FieldEntity referencedOn SignatureTable.fieldId
}
