package calcom.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RoleTable : IdTable<String>("\"Role\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val color = text("color").nullable()
    val description = text("description").nullable()
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Role_teamId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type = pgEnum("type", "\"RoleType\"", RoleType.entries, RoleType::dbName).default(RoleType.CUSTOM)
    override val primaryKey = PrimaryKey(id, name = "\"Role_pkey\"")

    init {
        index("\"Role_teamId_idx\"", false, teamId)
        uniqueIndex("\"Role_name_teamId_key\"", name, teamId)
    }
}
