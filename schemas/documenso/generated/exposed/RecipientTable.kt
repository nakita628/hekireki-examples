package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object RecipientTable : IdTable<Int>("\"Recipient\"") {
    override val id = integer("id").autoIncrement().entityId()
    val envelopeId =
        reference(
            "envelopeId",
            EnvelopeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Recipient_envelopeId_fkey\"",
        )
    val email = varchar("email", 255)
    val name = varchar("name", 255).default("")
    val token = text("token")
    val documentDeletedAt = pgTimestamp("documentDeletedAt", 3).nullable()
    val expired = pgTimestamp("expired", 3).nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val expirationNotifiedAt = pgTimestamp("expirationNotifiedAt", 3).nullable()
    val sentAt = pgTimestamp("sentAt", 3).nullable()
    val signedAt = pgTimestamp("signedAt", 3).nullable()
    val lastReminderSentAt = pgTimestamp("lastReminderSentAt", 3).nullable()
    val nextReminderAt = pgTimestamp("nextReminderAt", 3).nullable()
    val reminderCount = integer("reminderCount").default(0)
    val authOptions = jsonb<String>("authOptions", { it }, { it }).nullable()
    val signingOrder = integer("signingOrder").nullable()
    val rejectionReason = text("rejectionReason").nullable()
    val role = pgEnum("role", "\"RecipientRole\"", RecipientRole.entries, RecipientRole::dbName).default(RecipientRole.SIGNER)
    val readStatus = pgEnum("readStatus", "\"ReadStatus\"", ReadStatus.entries, ReadStatus::dbName).default(ReadStatus.NOT_OPENED)
    val signingStatus =
        pgEnum("signingStatus", "\"SigningStatus\"", SigningStatus.entries, SigningStatus::dbName).default(SigningStatus.NOT_SIGNED)
    val sendStatus = pgEnum("sendStatus", "\"SendStatus\"", SendStatus.entries, SendStatus::dbName).default(SendStatus.NOT_SENT)
    override val primaryKey = PrimaryKey(id, name = "\"Recipient_pkey\"")

    init {
        index("\"Recipient_token_idx\"", false, token)
        index("\"Recipient_email_idx\"", false, email)
        index("\"Recipient_envelopeId_idx\"", false, envelopeId)
        index("\"Recipient_signedAt_idx\"", false, signedAt)
        index("\"Recipient_expiresAt_idx\"", false, expiresAt)
        index("\"Recipient_email_documentDeletedAt_envelopeId_idx\"", false, email, documentDeletedAt, envelopeId)
        index("\"Recipient_email_envelopeId_idx\"", false, email, envelopeId)
        index("\"Recipient_email_signingStatus_envelopeId_role_idx\"", false, email, signingStatus, envelopeId, role)
        index("\"Recipient_nextReminderAt_idx\"", false, nextReminderAt)
        index("\"Recipient_email_trgm_idx\"", false, functions = listOf(indexExpression("\"email\" gin_trgm_ops")), indexType = "gin")
        index("\"Recipient_name_trgm_idx\"", false, functions = listOf(indexExpression("\"name\" gin_trgm_ops")), indexType = "gin")
    }
}
