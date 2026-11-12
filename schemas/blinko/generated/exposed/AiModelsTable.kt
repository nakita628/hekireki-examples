package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.time.Instant
import java.time.temporal.ChronoUnit

object AiModelsTable : IdTable<Int>("\"aiModels\"") {
    override val id = integer("id").autoIncrement().entityId()
    val providerId =
        reference(
            "providerId",
            AiProvidersTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"aiModels_providerId_fkey\"",
        )
    val title = varchar("title", 255)
    val modelKey = varchar("modelKey", 255)
    val capabilities = json<String>("capabilities", { it }, { it })
    val config = json<String>("config", { it }, { it }).nullable()
    val sortOrder = integer("sortOrder").default(0)
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"aiModels_pkey\"")

    init {
        index("\"aiModels_providerId_idx\"", false, providerId)
    }
}
