package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object McpToolTable : IdTable<String>("\"McpTool\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val description = text("description").nullable()
    val schema = jsonb<String>("schema", { it }, { it }).nullable()
    val isEnabled = bool("isEnabled").default(true)
    val isWrite = bool("isWrite").default(false)
    val connectionId =
        reference(
            "connectionId",
            McpConnectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"McpTool_connectionId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"McpTool_pkey\"")

    init {
        uniqueIndex("\"McpTool_connectionId_name_key\"", connectionId, name)
    }
}
