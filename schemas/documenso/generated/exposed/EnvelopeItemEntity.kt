package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class EnvelopeItemEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EnvelopeItemEntity>(EnvelopeItemTable)

    var title by EnvelopeItemTable.title
    var order by EnvelopeItemTable.order
    var documentDataId by EnvelopeItemTable.documentDataId
    var documentData by DocumentDataEntity referencedOn EnvelopeItemTable.documentDataId
    var envelopeId by EnvelopeItemTable.envelopeId
    var envelope by EnvelopeEntity referencedOn EnvelopeItemTable.envelopeId
    val field by FieldEntity referrersOn FieldTable.envelopeItemId
    val contents by EnvelopeContentEntity referrersOn EnvelopeContentTable.envelopeItemId
}
