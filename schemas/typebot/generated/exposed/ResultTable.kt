package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ResultTable : IdTable<String>("\"Result\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val typebotId =
        reference(
            "typebotId",
            TypebotTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Result_typebotId_fkey\"",
        )
    val variables = jsonb<String>("variables", { it }, { it })
    val isCompleted = bool("isCompleted")
    val hasStarted = bool("hasStarted").nullable()
    val isArchived = bool("isArchived").nullable().default(false)
    val lastChatSessionId = text("lastChatSessionId").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Result_pkey\"")

    init {
        index(
            "\"Result_typebotId_hasStarted_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"typebotId\""), indexExpression("\"hasStarted\""), indexExpression("\"createdAt\" DESC")),
        )
        index("\"Result_typebotId_isCompleted_idx\"", false, typebotId, isCompleted)
    }
}
