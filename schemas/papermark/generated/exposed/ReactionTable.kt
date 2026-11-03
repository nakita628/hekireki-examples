package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ReactionTable : IdTable<String>("\"Reaction\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val viewId =
        reference(
            "viewId",
            ViewTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Reaction_viewId_fkey\"",
        )
    val pageNumber = integer("pageNumber")
    val type = text("type")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Reaction_pkey\"")

    init {
        index("\"Reaction_viewId_idx\"", false, viewId)
        index("\"Reaction_viewId_type_idx\"", false, viewId, type)
    }
}
