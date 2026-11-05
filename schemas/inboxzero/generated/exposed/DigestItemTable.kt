package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DigestItemTable : IdTable<String>("\"DigestItem\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val messageId = text("messageId")
    val threadId = text("threadId")
    val content = text("content")
    val digestId =
        reference(
            "digestId",
            DigestTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DigestItem_digestId_fkey\"",
        )
    val actionId =
        optReference(
            "actionId",
            ExecutedActionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DigestItem_actionId_fkey\"",
        )
    val coldEmailId =
        optReference(
            "coldEmailId",
            ColdEmailTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DigestItem_coldEmailId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"DigestItem_pkey\"")

    init {
        index("\"DigestItem_actionId_idx\"", false, actionId)
        index("\"DigestItem_coldEmailId_idx\"", false, coldEmailId)
        uniqueIndex("\"DigestItem_digestId_threadId_messageId_key\"", digestId, threadId, messageId)
    }
}
