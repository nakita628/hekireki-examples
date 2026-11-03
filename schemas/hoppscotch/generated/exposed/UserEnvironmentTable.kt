package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object UserEnvironmentTable : IdTable<String>("\"UserEnvironment\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userUid =
        reference(
            "userUid",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserEnvironment_userUid_fkey\"",
        )
    val name = text("name").nullable()
    val variables = jsonb<String>("variables", { it }, { it })
    val isGlobal = bool("isGlobal")
    override val primaryKey = PrimaryKey(id, name = "\"UserEnvironment_pkey\"")
}
