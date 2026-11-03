package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object EnvelopeItemTable : IdTable<String>("\"EnvelopeItem\"") {
    override val id = text("id").entityId()
    val title = text("title")
    val order = integer("order")
    val documentDataId =
        reference(
            "documentDataId",
            DocumentDataTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EnvelopeItem_documentDataId_fkey\"",
        ).uniqueIndex("\"EnvelopeItem_documentDataId_key\"")
    val envelopeId =
        reference(
            "envelopeId",
            EnvelopeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EnvelopeItem_envelopeId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"EnvelopeItem_pkey\"")

    init {
        index("\"EnvelopeItem_envelopeId_idx\"", false, envelopeId)
    }
}
