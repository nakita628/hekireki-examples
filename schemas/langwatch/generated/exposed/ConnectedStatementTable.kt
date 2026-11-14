package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ConnectedStatementTable : IdTable<String>("\"ConnectedStatement\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val accountId = text("accountId")
    val month = pgTimestamp("month", 3)
    val sentAt = pgTimestamp("sentAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"ConnectedStatement_pkey\"")

    init {
        index("\"ConnectedStatement_accountId_idx\"", false, accountId)
        uniqueIndex("\"ConnectedStatement_accountId_month_key\"", accountId, month)
    }
}
