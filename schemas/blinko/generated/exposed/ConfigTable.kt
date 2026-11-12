package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json

object ConfigTable : IdTable<Int>("config") {
    override val id = integer("id").autoIncrement().entityId()
    val key = pgVarchar("key").default("")
    val config = json<String>("config", { it }, { it }).nullable()
    val userId =
        optReference(
            "userId",
            AccountsTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"config_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "config_pkey")
}
