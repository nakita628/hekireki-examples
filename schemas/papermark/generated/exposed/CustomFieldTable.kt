package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CustomFieldTable : IdTable<String>("\"CustomField\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type = pgEnum("type", "\"CustomFieldType\"", CustomFieldType.entries, CustomFieldType::dbName)
    val identifier = text("identifier")
    val label = text("label")
    val placeholder = text("placeholder").nullable()
    val required = bool("required").default(false)
    val disabled = bool("disabled").default(false)
    val linkId =
        reference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CustomField_linkId_fkey\"",
        )
    val orderIndex = integer("orderIndex").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"CustomField_pkey\"")

    init {
        index("\"CustomField_linkId_idx\"", false, linkId)
    }
}
