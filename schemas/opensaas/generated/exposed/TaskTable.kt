package opensaas.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object TaskTable : IdTable<String>("\"Task\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Task_userId_fkey\"",
        )
    val description = text("description")
    val time = text("time").default("1")
    val isDone = bool("isDone").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"Task_pkey\"")
}
