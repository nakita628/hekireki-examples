package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ColdEmailTable : IdTable<String>("\"ColdEmail\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val fromEmail = text("fromEmail")
    val messageId = text("messageId").nullable()
    val threadId = text("threadId").nullable()
    val status = pgEnum("status", "\"ColdEmailStatus\"", ColdEmailStatus.entries, ColdEmailStatus::dbName).nullable()
    val reason = text("reason").nullable()
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ColdEmail_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ColdEmail_pkey\"")

    init {
        index("\"ColdEmail_emailAccountId_status_idx\"", false, emailAccountId, status)
        index("\"ColdEmail_emailAccountId_createdAt_idx\"", false, emailAccountId, createdAt)
        uniqueIndex("\"ColdEmail_emailAccountId_fromEmail_key\"", emailAccountId, fromEmail)
    }
}
