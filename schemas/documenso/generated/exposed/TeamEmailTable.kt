package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TeamEmailTable : IdTable<Int>("\"TeamEmail\"") {
    override val id =
        integer("teamId")
            .references(
                TeamTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"TeamEmail_teamId_fkey\"",
            ).entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val email = text("email").uniqueIndex("\"TeamEmail_email_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"TeamEmail_pkey\"")

    init {
        uniqueIndex("\"TeamEmail_teamId_key\"", id)
    }
}
