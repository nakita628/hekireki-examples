package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object CounterTable : IdTable<String>("\"Counter\"") {
    override val id = text("id").entityId()
    val value = integer("value")
    override val primaryKey = PrimaryKey(id, name = "\"Counter_pkey\"")
}
