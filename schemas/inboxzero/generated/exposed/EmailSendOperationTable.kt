package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EmailSendOperationTable : IdTable<String>("\"EmailSendOperation\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val clientMutationId = text("clientMutationId")
    val payloadHash = text("payloadHash")
    val status =
        pgEnum("status", "\"EmailSendOperationStatus\"", EmailSendOperationStatus.entries, EmailSendOperationStatus::dbName).default(
            EmailSendOperationStatus.PROCESSING,
        )
    val processingStartedAt =
        pgTimestamp("processingStartedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val result = jsonb<String>("result", { it }, { it }).nullable()
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EmailSendOperation_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"EmailSendOperation_pkey\"")

    init {
        index("\"EmailSendOperation_status_updatedAt_idx\"", false, status, updatedAt)
        uniqueIndex("\"EmailSendOperation_emailAccountId_clientMutationId_key\"", emailAccountId, clientMutationId)
    }
}
