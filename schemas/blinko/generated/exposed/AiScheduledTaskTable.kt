package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.time.Instant
import java.time.temporal.ChronoUnit

object AiScheduledTaskTable : IdTable<Int>("\"aiScheduledTask\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = varchar("name", 255)
    val prompt = text("prompt")
    val schedule = varchar("schedule", 100)
    val isEnabled = bool("isEnabled").default(true)
    val lastRun = pgTimestamptz("lastRun", 6).nullable()
    val lastResult = json<String>("lastResult", { it }, { it }).nullable()
    val accountId =
        reference(
            "accountId",
            AccountsTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"aiScheduledTask_accountId_fkey\"",
        )
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"aiScheduledTask_pkey\"")

    init {
        index("\"aiScheduledTask_accountId_idx\"", false, accountId)
    }
}
