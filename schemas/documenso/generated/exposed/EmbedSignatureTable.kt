package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object EmbedSignatureTable : IdTable<String>("\"EmbedSignature\"") {
    override val id = text("key").entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organisationId =
        reference(
            "organisationId",
            OrganisationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EmbedSignature_organisationId_fkey\"",
        )
    val recipientId = integer("recipientId")
    val signature = text("signature")
    override val primaryKey = PrimaryKey(id, name = "\"EmbedSignature_pkey\"")

    init {
        index("\"EmbedSignature_organisationId_idx\"", false, organisationId)
        index("\"EmbedSignature_recipientId_idx\"", false, recipientId)
    }
}
