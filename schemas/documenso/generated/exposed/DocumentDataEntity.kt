package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DocumentDataEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DocumentDataEntity>(DocumentDataTable)

    var type by DocumentDataTable.type
    var data by DocumentDataTable.data
    var initialData by DocumentDataTable.initialData
    val envelopeItem by EnvelopeItemEntity optionalBackReferencedOn EnvelopeItemTable.documentDataId
}
