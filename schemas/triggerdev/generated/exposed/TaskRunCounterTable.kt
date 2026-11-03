package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object TaskRunCounterTable : IdTable<String>("\"TaskRunCounter\"") {
    override val id = text("taskIdentifier").entityId()
    val lastNumber = integer("lastNumber").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"TaskRunCounter_pkey\"")
}
