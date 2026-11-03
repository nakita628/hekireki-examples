package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConversationViewTable : IdTable<String>("\"ConversationView\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val conversationId =
        reference(
            "conversationId",
            ConversationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConversationView_conversationId_fkey\"",
        )
    val viewId =
        reference(
            "viewId",
            ViewTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConversationView_viewId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ConversationView_pkey\"")

    init {
        index("\"ConversationView_conversationId_idx\"", false, conversationId)
        index("\"ConversationView_viewId_idx\"", false, viewId)
        uniqueIndex("\"ConversationView_conversationId_viewId_key\"", conversationId, viewId)
    }
}
