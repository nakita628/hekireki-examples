package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object TeamTable : IdTable<String>("\"Team\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    override val primaryKey = PrimaryKey(id, name = "\"Team_pkey\"")
}
