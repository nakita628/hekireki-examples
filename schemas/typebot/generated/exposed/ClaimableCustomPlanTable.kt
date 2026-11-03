package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ClaimableCustomPlanTable : IdTable<String>("\"ClaimableCustomPlan\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val claimedAt = pgTimestamp("claimedAt", 3).nullable()
    val name = text("name")
    val description = text("description").nullable()
    val price = integer("price")
    val currency = text("currency")
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ClaimableCustomPlan_workspaceId_fkey\"",
        ).uniqueIndex("\"ClaimableCustomPlan_workspaceId_key\"")
    val chatsLimit = integer("chatsLimit")
    val storageLimit = integer("storageLimit")
    val seatsLimit = integer("seatsLimit")
    val isYearly = bool("isYearly").default(false)
    val companyName = text("companyName").nullable()
    val vatType = text("vatType").nullable()
    val vatValue = text("vatValue").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"ClaimableCustomPlan_pkey\"")
}
