package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MailSplitTable : IdTable<String>("\"MailSplit\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val order = integer("order")
    val matchAll = bool("matchAll").default(true)
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MailSplit_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"MailSplit_pkey\"")

    init {
        index("\"MailSplit_emailAccountId_order_idx\"", false, emailAccountId, order)
        uniqueIndex("\"MailSplit_emailAccountId_name_key\"", emailAccountId, name)
    }
}
