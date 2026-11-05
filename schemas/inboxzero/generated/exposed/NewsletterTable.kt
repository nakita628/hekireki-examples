package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object NewsletterTable : IdTable<String>("\"Newsletter\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val email = text("email")
    val name = text("name").nullable()
    val status = pgEnum("status", "\"NewsletterStatus\"", NewsletterStatus.entries, NewsletterStatus::dbName).nullable()
    val patternAnalyzed = bool("patternAnalyzed").default(false)
    val lastAnalyzedAt = pgTimestamp("lastAnalyzedAt", 3).nullable()
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Newsletter_emailAccountId_fkey\"",
        )
    val categoryId =
        optReference(
            "categoryId",
            CategoryTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Newsletter_categoryId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Newsletter_pkey\"")

    init {
        index("\"Newsletter_emailAccountId_status_idx\"", false, emailAccountId, status)
        index("\"Newsletter_categoryId_idx\"", false, categoryId)
        uniqueIndex("\"Newsletter_email_emailAccountId_key\"", email, emailAccountId)
    }
}
