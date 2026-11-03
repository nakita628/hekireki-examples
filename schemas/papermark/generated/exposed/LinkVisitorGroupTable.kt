package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LinkVisitorGroupTable : IdTable<String>("\"LinkVisitorGroup\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val linkId =
        reference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LinkVisitorGroup_linkId_fkey\"",
        )
    val visitorGroupId =
        reference(
            "visitorGroupId",
            VisitorGroupTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LinkVisitorGroup_visitorGroupId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LinkVisitorGroup_pkey\"")

    init {
        index("\"LinkVisitorGroup_linkId_idx\"", false, linkId)
        index("\"LinkVisitorGroup_visitorGroupId_idx\"", false, visitorGroupId)
        uniqueIndex("\"LinkVisitorGroup_linkId_visitorGroupId_key\"", linkId, visitorGroupId)
    }
}
