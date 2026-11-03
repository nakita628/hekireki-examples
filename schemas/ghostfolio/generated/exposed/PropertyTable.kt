package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object PropertyTable : IdTable<String>("\"Property\"") {
    override val id = text("key").entityId()
    val value = text("value")
    override val primaryKey = PrimaryKey(id, name = "\"Property_pkey\"")
}
