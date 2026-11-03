package linkwarden.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LinkTable : IdTable<Int>("\"Link\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = text("name").default("")
    val type = text("type").default("url")
    val description = text("description").default("")
    val createdById =
        optReference(
            "createdById",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Link_createdById_fkey\"",
        )
    val collectionId =
        reference(
            "collectionId",
            CollectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Link_collectionId_fkey\"",
        )
    val icon = text("icon").nullable()
    val iconWeight = text("iconWeight").nullable()
    val color = text("color").nullable()
    val url = text("url").nullable()
    val textContent = text("textContent").nullable()
    val preview = text("preview").nullable()
    val image = text("image").nullable()
    val pdf = text("pdf").nullable()
    val readable = text("readable").nullable()
    val monolith = text("monolith").nullable()
    val clientSide = bool("clientSide").default(false)
    val aiTagged = bool("aiTagged").default(false)
    val metaDescription = text("metaDescription").nullable()
    val indexVersion = integer("indexVersion").nullable()
    val lastPreserved = pgTimestamp("lastPreserved", 3).nullable()
    val importDate = pgTimestamp("importDate", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Link_pkey\"")

    init {
        index("\"Link_collectionId_idx\"", false, collectionId)
    }
}
