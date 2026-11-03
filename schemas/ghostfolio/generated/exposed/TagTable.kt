package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object TagTable : IdTable<String>("\"Tag\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Tag_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Tag_pkey\"")

    init {
        index("\"Tag_name_idx\"", false, name)
        uniqueIndex("\"Tag_name_userId_key\"", name, userId)
    }
}
