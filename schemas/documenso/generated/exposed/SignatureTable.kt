package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SignatureTable : IdTable<Int>("\"Signature\"") {
    override val id = integer("id").autoIncrement().entityId()
    val created = pgTimestamp("created", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val recipientId =
        reference(
            "recipientId",
            RecipientTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Signature_recipientId_fkey\"",
        )
    val fieldId =
        reference(
            "fieldId",
            FieldTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Signature_fieldId_fkey\"",
        ).uniqueIndex("\"Signature_fieldId_key\"")
    val signatureImageAsBase64 = text("signatureImageAsBase64").nullable()
    val typedSignature = text("typedSignature").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Signature_pkey\"")

    init {
        index("\"Signature_recipientId_idx\"", false, recipientId)
    }
}
