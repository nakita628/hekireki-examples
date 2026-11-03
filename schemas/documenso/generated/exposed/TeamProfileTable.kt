package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object TeamProfileTable : IdTable<String>("\"TeamProfile\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val enabled = bool("enabled").default(false)
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamProfile_teamId_fkey\"",
        ).uniqueIndex("\"TeamProfile_teamId_key\"")
    val bio = text("bio").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"TeamProfile_pkey\"")
}
