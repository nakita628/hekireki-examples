package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object EnvelopeAttachmentTable : IdTable<String>("\"EnvelopeAttachment\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val type = text("type")
    val label = text("label")
    val data = text("data")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val envelopeId =
        reference(
            "envelopeId",
            EnvelopeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EnvelopeAttachment_envelopeId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"EnvelopeAttachment_pkey\"")

    init {
        index("\"EnvelopeAttachment_envelopeId_idx\"", false, envelopeId)
    }
}
