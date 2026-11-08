package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class EnvelopeContentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EnvelopeContentEntity>(EnvelopeContentTable)

    var envelopeId by EnvelopeContentTable.envelopeId
    var envelopeItemId by EnvelopeContentTable.envelopeItemId
    var envelopeItem by EnvelopeItemEntity referencedOn EnvelopeContentTable.envelopeItemId
    var envelope by EnvelopeEntity referencedOn EnvelopeContentTable.envelopeId
    var contentMeta by EnvelopeContentTable.contentMeta
    var dataContentId by EnvelopeContentTable.dataContentId
    var dataContent by DataContentEntity optionalReferencedOn EnvelopeContentTable.dataContentId
}
