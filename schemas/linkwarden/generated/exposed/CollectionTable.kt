package linkwarden.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CollectionTable : IdTable<Int>("\"Collection\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = text("name")
    val description = text("description").default("")
    val icon = text("icon").nullable()
    val iconWeight = text("iconWeight").nullable()
    val color = text("color").default("#0ea5e9")
    val parentId =
        optReference(
            "parentId",
            CollectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Collection_parentId_fkey\"",
        )
    val isPublic = bool("isPublic").default(false)
    val ownerId =
        reference(
            "ownerId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Collection_ownerId_fkey\"",
        )
    val createdById =
        optReference(
            "createdById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Collection_createdById_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Collection_pkey\"")

    init {
        index("\"Collection_ownerId_idx\"", false, ownerId)
    }
}
