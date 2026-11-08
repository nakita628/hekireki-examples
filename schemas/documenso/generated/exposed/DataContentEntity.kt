package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DataContentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataContentEntity>(DataContentTable)

    var type by DataContentTable.type
    var data by DataContentTable.data
    var metadata by DataContentTable.metadata
    val envelopeContents by EnvelopeContentEntity optionalReferrersOn EnvelopeContentTable.dataContentId
}
