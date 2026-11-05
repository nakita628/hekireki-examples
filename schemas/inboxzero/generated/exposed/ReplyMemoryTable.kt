package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ReplyMemoryTable : IdTable<String>("\"ReplyMemory\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val content = text("content")
    val kind = pgEnum("kind", "\"ReplyMemoryKind\"", ReplyMemoryKind.entries, ReplyMemoryKind::dbName)
    val scopeType = pgEnum("scopeType", "\"ReplyMemoryScopeType\"", ReplyMemoryScopeType.entries, ReplyMemoryScopeType::dbName)
    val scopeValue = text("scopeValue")
    val isLearnedStyleEvidence = bool("isLearnedStyleEvidence").default(false)
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ReplyMemory_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ReplyMemory_pkey\"")

    init {
        index("\"ReplyMemory_emailAccountId_updatedAt_idx\"", false, emailAccountId, updatedAt)
        index("\"ReplyMemory_emailAccountId_scopeType_scopeValue_idx\"", false, emailAccountId, scopeType, scopeValue)
        uniqueIndex("ReplyMemory_emailAccountId_kind_scopeType_scopeValue_conten_key", emailAccountId, kind, scopeType, scopeValue, content)
    }
}
