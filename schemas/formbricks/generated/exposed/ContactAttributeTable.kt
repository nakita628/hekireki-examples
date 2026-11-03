package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ContactAttributeTable : IdTable<String>("\"ContactAttribute\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val attributeKeyId =
        reference(
            "attributeKeyId",
            ContactAttributeKeyTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ContactAttribute_attributeKeyId_fkey\"",
        )
    val contactId =
        reference(
            "contactId",
            ContactTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ContactAttribute_contactId_fkey\"",
        )
    val value = text("value")
    val valueNumber = double("valueNumber").nullable()
    val valueDate = pgTimestamp("valueDate", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"ContactAttribute_pkey\"")

    init {
        index("\"ContactAttribute_attributeKeyId_value_idx\"", false, attributeKeyId, value)
        index("\"ContactAttribute_attributeKeyId_valueNumber_idx\"", false, attributeKeyId, valueNumber)
        index("\"ContactAttribute_attributeKeyId_valueDate_idx\"", false, attributeKeyId, valueDate)
        uniqueIndex("\"ContactAttribute_contactId_attributeKeyId_key\"", contactId, attributeKeyId)
    }
}
