package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LogTable : IdTable<String>("\"Log\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val resultId =
        reference(
            "resultId",
            ResultTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Log_resultId_fkey\"",
        )
    val status = text("status")
    val context = text("context").nullable()
    val description = text("description")
    val details = text("details").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Log_pkey\"")

    init {
        index("\"Log_resultId_idx\"", false, resultId)
    }
}
