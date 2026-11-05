package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationRuleActionTable : IdTable<String>("\"OrganizationRuleAction\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organizationRuleId =
        reference(
            "organizationRuleId",
            OrganizationRuleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganizationRuleAction_organizationRuleId_fkey\"",
        )
    val type = pgEnum("type", "\"ActionType\"", ActionType.entries, ActionType::dbName)
    val label = text("label").nullable()
    val subject = text("subject").nullable()
    val content = text("content").nullable()
    val to = text("to").nullable()
    val cc = text("cc").nullable()
    val bcc = text("bcc").nullable()
    val url = text("url").nullable()
    val folderName = text("folderName").nullable()
    val delayInMinutes = integer("delayInMinutes").nullable()
    val staticAttachments = jsonb<String>("staticAttachments", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationRuleAction_pkey\"")

    init {
        index("\"OrganizationRuleAction_organizationRuleId_idx\"", false, organizationRuleId)
    }
}
