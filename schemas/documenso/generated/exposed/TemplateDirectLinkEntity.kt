package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TemplateDirectLinkEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TemplateDirectLinkEntity>(TemplateDirectLinkTable)

    var envelopeId by TemplateDirectLinkTable.envelopeId
    var token by TemplateDirectLinkTable.token
    var createdAt by TemplateDirectLinkTable.createdAt
    var enabled by TemplateDirectLinkTable.enabled
    var directTemplateRecipientId by TemplateDirectLinkTable.directTemplateRecipientId
    var envelope by EnvelopeEntity referencedOn TemplateDirectLinkTable.envelopeId
}
