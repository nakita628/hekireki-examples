package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DataroomTable : IdTable<String>("\"Dataroom\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val pId = text("pId").uniqueIndex("\"Dataroom_pId_key\"")
    val name = text("name")
    val internalName = text("internalName").nullable()
    val description = text("description").nullable()
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Dataroom_teamId_fkey\"",
        )
    val brandId =
        optReference(
            "brandId",
            BrandTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Dataroom_brandId_fkey\"",
        )
    val conversationsEnabled = bool("conversationsEnabled").default(false)
    val agentsEnabled = bool("agentsEnabled").default(false)
    val vectorStoreId = text("vectorStoreId").nullable()
    val requestListEnabled = bool("requestListEnabled").default(false)
    val enableChangeNotifications = bool("enableChangeNotifications").default(false)
    val enableVisitorUploadChangeNotifications = bool("enableVisitorUploadChangeNotifications").default(false)
    val defaultPermissionStrategy =
        pgEnum(
            "defaultPermissionStrategy",
            "\"DefaultPermissionStrategy\"",
            DefaultPermissionStrategy.entries,
            DefaultPermissionStrategy::dbName,
        ).default(DefaultPermissionStrategy.INHERIT_FROM_PARENT)
    val defaultShowBanner = bool("defaultShowBanner").default(true)
    val defaultGroupPermissionStrategy =
        pgEnum(
            "defaultGroupPermissionStrategy",
            "\"DefaultPermissionStrategy\"",
            DefaultPermissionStrategy.entries,
            DefaultPermissionStrategy::dbName,
        ).default(DefaultPermissionStrategy.INHERIT_FROM_PARENT)
    val defaultRootItemAccess =
        pgEnum("defaultRootItemAccess", "\"RootItemAccess\"", RootItemAccess.entries, RootItemAccess::dbName).default(
            RootItemAccess.VIEW_ONLY,
        )
    val defaultGroupRootItemAccess =
        pgEnum("defaultGroupRootItemAccess", "\"RootItemAccess\"", RootItemAccess.entries, RootItemAccess::dbName).default(
            RootItemAccess.VIEW_ONLY,
        )
    val allowBulkDownload = bool("allowBulkDownload").default(true)
    val showLastUpdated = bool("showLastUpdated").default(true)
    val introductionEnabled = bool("introductionEnabled").default(false)
    val introductionContent = jsonb<String>("introductionContent", { it }, { it }).nullable()
    val isFrozen = bool("isFrozen").default(false)
    val frozenAt = pgTimestamp("frozenAt", 3).nullable()
    val frozenBy =
        optReference(
            "frozenBy",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Dataroom_frozenBy_fkey\"",
        )
    val freezeArchiveUrl = text("freezeArchiveUrl").nullable()
    val freezeArchiveHash = text("freezeArchiveHash").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Dataroom_pkey\"")

    init {
        index("\"Dataroom_teamId_idx\"", false, teamId)
        index("\"Dataroom_brandId_idx\"", false, brandId)
    }
}
