package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object MailSplitFilterTable : IdTable<String>("\"MailSplitFilter\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val kind = pgEnum("kind", "\"MailSplitFilterKind\"", MailSplitFilterKind.entries, MailSplitFilterKind::dbName)
    val value = text("value").nullable()
    val order = integer("order")
    val mailSplitId =
        reference(
            "mailSplitId",
            MailSplitTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MailSplitFilter_mailSplitId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"MailSplitFilter_pkey\"")

    init {
        index("\"MailSplitFilter_mailSplitId_idx\"", false, mailSplitId)
        index("\"MailSplitFilter_kind_value_idx\"", false, kind, value)
        uniqueIndex("\"MailSplitFilter_mailSplitId_order_key\"", mailSplitId, order)
    }
}
