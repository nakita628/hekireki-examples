package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CategoryTable : IdTable<String>("\"Category\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val description = text("description").nullable()
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Category_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Category_pkey\"")

    init {
        index("\"Category_emailAccountId_idx\"", false, emailAccountId)
        uniqueIndex("\"Category_name_emailAccountId_key\"", name, emailAccountId)
    }
}
