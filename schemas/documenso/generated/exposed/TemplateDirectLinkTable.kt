package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TemplateDirectLinkTable : IdTable<String>("\"TemplateDirectLink\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val envelopeId =
        reference(
            "envelopeId",
            EnvelopeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TemplateDirectLink_envelopeId_fkey\"",
        ).uniqueIndex("\"TemplateDirectLink_envelopeId_key\"")
    val token = text("token").uniqueIndex("\"TemplateDirectLink_token_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val enabled = bool("enabled")
    val directTemplateRecipientId = integer("directTemplateRecipientId")
    override val primaryKey = PrimaryKey(id, name = "\"TemplateDirectLink_pkey\"")

    init {
        uniqueIndex("\"TemplateDirectLink_id_key\"", id)
    }
}
