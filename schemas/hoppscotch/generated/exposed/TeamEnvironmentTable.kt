package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object TeamEnvironmentTable : IdTable<String>("\"TeamEnvironment\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val teamID =
        reference(
            "teamID",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamEnvironment_teamID_fkey\"",
        )
    val name = text("name")
    val variables = jsonb<String>("variables", { it }, { it })
    override val primaryKey = PrimaryKey(id, name = "\"TeamEnvironment_pkey\"")
}
