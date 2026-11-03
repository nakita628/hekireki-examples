package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object CscSessionTable : IdTable<String>("\"CscSession\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val envelopeId = text("envelopeId")
    val signingTime = pgTimestamp("signingTime", 3)
    val itemsJson = jsonb<String>("itemsJson", { it }, { it })
    val encryptedSad = binary("encryptedSad").nullable()
    val sadExpiresAt = pgTimestamp("sadExpiresAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val recipientId =
        reference(
            "recipientId",
            RecipientTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CscSession_recipientId_fkey\"",
        ).uniqueIndex("\"CscSession_recipientId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"CscSession_pkey\"")
}
