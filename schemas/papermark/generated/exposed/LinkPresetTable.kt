package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LinkPresetTable : IdTable<String>("\"LinkPreset\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LinkPreset_teamId_fkey\"",
        )
    val pId = text("pId").nullable().uniqueIndex("\"LinkPreset_pId_key\"")
    val enableCustomMetaTag = bool("enableCustomMetaTag").nullable().default(false)
    val metaTitle = text("metaTitle").nullable()
    val metaDescription = text("metaDescription").nullable()
    val metaImage = text("metaImage").nullable()
    val metaFavicon = text("metaFavicon").nullable()
    val enableNotification = bool("enableNotification").nullable().default(false)
    val emailProtected = bool("emailProtected").nullable().default(true)
    val emailAuthenticated = bool("emailAuthenticated").nullable().default(false)
    val allowDownload = bool("allowDownload").nullable().default(false)
    val enableAllowList = bool("enableAllowList").nullable().default(false)
    val allowList = array("allowList", TextColumnType()).nullable()
    val enableDenyList = bool("enableDenyList").nullable().default(false)
    val denyList = array("denyList", TextColumnType()).nullable()
    val expiresIn = integer("expiresIn").nullable()
    val enableScreenshotProtection = bool("enableScreenshotProtection").nullable().default(false)
    val enableConfidentialView = bool("enableConfidentialView").nullable().default(false)
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val enablePassword = bool("enablePassword").nullable().default(false)
    val password = text("password").nullable()
    val enableWatermark = bool("enableWatermark").nullable().default(false)
    val watermarkConfig = jsonb<String>("watermarkConfig", { it }, { it }).nullable()
    val enableAgreement = bool("enableAgreement").nullable().default(false)
    val agreementId = text("agreementId").nullable()
    val enableCustomFields = bool("enableCustomFields").nullable().default(false)
    val customFields = jsonb<String>("customFields", { it }, { it }).nullable()
    val showBanner = bool("showBanner").nullable().default(false)
    val welcomeMessage = text("welcomeMessage").nullable()
    val isDefault = bool("isDefault").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LinkPreset_pkey\"")

    init {
        index("\"LinkPreset_teamId_idx\"", false, teamId)
    }
}
