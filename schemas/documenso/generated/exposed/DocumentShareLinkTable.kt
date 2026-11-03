package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DocumentShareLinkTable : IdTable<Int>("\"DocumentShareLink\"") {
    override val id = integer("id").autoIncrement().entityId()
    val email = text("email")
    val slug = text("slug").uniqueIndex("\"DocumentShareLink_slug_key\"")
    val envelopeId =
        reference(
            "envelopeId",
            EnvelopeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentShareLink_envelopeId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DocumentShareLink_pkey\"")

    init {
        uniqueIndex("\"DocumentShareLink_envelopeId_email_key\"", envelopeId, email)
    }
}
