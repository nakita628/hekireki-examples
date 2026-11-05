package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LabelTable : IdTable<String>("\"Label\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val gmailLabelId = text("gmailLabelId")
    val name = text("name")
    val description = text("description").nullable()
    val enabled = bool("enabled").default(true)
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Label_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Label_pkey\"")

    init {
        index("\"Label_emailAccountId_idx\"", false, emailAccountId)
        uniqueIndex("\"Label_gmailLabelId_emailAccountId_key\"", gmailLabelId, emailAccountId)
        uniqueIndex("\"Label_name_emailAccountId_key\"", name, emailAccountId)
    }
}
