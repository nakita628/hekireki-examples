package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CloudSpendAlertTable : IdTable<String>("cloud_spend_alerts") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val orgId =
        reference(
            "org_id",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "cloud_spend_alerts_org_id_fkey",
        )
    val title = text("title")
    val threshold = decimal("threshold", 65, 30)
    val triggeredAt = pgTimestamp("triggered_at", 3).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "cloud_spend_alerts_pkey")

    init {
        index("cloud_spend_alerts_org_id_idx", false, orgId)
    }
}
