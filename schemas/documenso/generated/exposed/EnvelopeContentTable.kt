package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object EnvelopeContentTable : IdTable<String>("\"EnvelopeContent\"") {
    override val id = text("id").entityId()
    val envelopeId =
        reference(
            "envelopeId",
            EnvelopeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EnvelopeContent_envelopeId_fkey\"",
        )
    val envelopeItemId =
        reference(
            "envelopeItemId",
            EnvelopeItemTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EnvelopeContent_envelopeItemId_fkey\"",
        )
    val contentMeta = jsonb<String>("contentMeta", { it }, { it })
    val dataContentId =
        optReference(
            "dataContentId",
            DataContentTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EnvelopeContent_dataContentId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"EnvelopeContent_pkey\"")

    init {
        index("\"EnvelopeContent_envelopeId_idx\"", false, envelopeId)
        index("\"EnvelopeContent_envelopeItemId_idx\"", false, envelopeItemId)
    }
}
