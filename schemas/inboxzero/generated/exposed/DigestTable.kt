package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DigestTable : IdTable<String>("\"Digest\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Digest_emailAccountId_fkey\"",
        )
    val sentAt = pgTimestamp("sentAt", 3).nullable()
    val status = pgEnum("status", "\"DigestStatus\"", DigestStatus.entries, DigestStatus::dbName).default(DigestStatus.PENDING)
    override val primaryKey = PrimaryKey(id, name = "\"Digest_pkey\"")

    init {
        index("\"Digest_emailAccountId_idx\"", false, emailAccountId)
    }
}
