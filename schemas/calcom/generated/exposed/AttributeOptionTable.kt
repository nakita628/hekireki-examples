package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object AttributeOptionTable : IdTable<String>("\"AttributeOption\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val attributeId =
        reference(
            "attributeId",
            AttributeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttributeOption_attributeId_fkey\"",
        )
    val value = text("value")
    val slug = text("slug")
    val isGroup = bool("isGroup").default(false)
    val contains = array("contains", TextColumnType()).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"AttributeOption_pkey\"")
}
