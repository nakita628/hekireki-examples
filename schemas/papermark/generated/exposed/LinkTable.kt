package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LinkTable : IdTable<String>("\"Link\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val documentId =
        optReference(
            "documentId",
            DocumentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Link_documentId_fkey\"",
        )
    val dataroomId =
        optReference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Link_dataroomId_fkey\"",
        )
    val linkType = pgEnum("linkType", "\"LinkType\"", LinkType.entries, LinkType::dbName).default(LinkType.DOCUMENT_LINK)
    val url = text("url").nullable().uniqueIndex("\"Link_url_key\"")
    val name = text("name").nullable()
    val slug = text("slug").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val password = text("password").nullable()
    val allowList = array("allowList", TextColumnType()).nullable()
    val denyList = array("denyList", TextColumnType()).nullable()
    val emailProtected = bool("emailProtected").default(true)
    val emailAuthenticated = bool("emailAuthenticated").default(false)
    val allowDownload = bool("allowDownload").nullable().default(false)
    val isArchived = bool("isArchived").default(false)
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val domainId =
        optReference(
            "domainId",
            DomainTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Link_domainId_fkey\"",
        )
    val domainSlug = text("domainSlug").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val enableNotification = bool("enableNotification").nullable().default(true)
    val enableFeedback = bool("enableFeedback").nullable().default(false)
    val enableQuestion = bool("enableQuestion").nullable().default(false)
    val enableScreenshotProtection = bool("enableScreenshotProtection").nullable().default(false)
    val enableConfidentialView = bool("enableConfidentialView").nullable().default(false)
    val enableAgreement = bool("enableAgreement").nullable().default(false)
    val agreementId =
        optReference(
            "agreementId",
            AgreementTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Link_agreementId_fkey\"",
        )
    val showBanner = bool("showBanner").nullable().default(false)
    val enableWatermark = bool("enableWatermark").nullable().default(false)
    val watermarkConfig = jsonb<String>("watermarkConfig", { it }, { it }).nullable()
    val audienceType =
        pgEnum("audienceType", "\"LinkAudienceType\"", LinkAudienceType.entries, LinkAudienceType::dbName).default(LinkAudienceType.GENERAL)
    val groupId =
        optReference(
            "groupId",
            ViewerGroupTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Link_groupId_fkey\"",
        )
    val permissionGroupId =
        optReference(
            "permissionGroupId",
            PermissionGroupTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Link_permissionGroupId_fkey\"",
        )
    val metaTitle = text("metaTitle").nullable()
    val metaDescription = text("metaDescription").nullable()
    val metaImage = text("metaImage").nullable()
    val metaFavicon = text("metaFavicon").nullable()
    val enableCustomMetatag = bool("enableCustomMetatag").nullable().default(false)
    val welcomeMessage = text("welcomeMessage").nullable()
    val enableConversation = bool("enableConversation").default(false)
    val enableAIAgents = bool("enableAIAgents").nullable().default(false)
    val enableUpload = bool("enableUpload").nullable().default(false)
    val isFileRequestOnly = bool("isFileRequestOnly").nullable().default(false)
    val uploadFolderId = text("uploadFolderId").nullable()
    val uploadFolderIds = array("uploadFolderIds", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val enableIndexFile = bool("enableIndexFile").nullable().default(false)
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Link_teamId_fkey\"",
        )
    val brandId =
        optReference(
            "brandId",
            BrandTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Link_brandId_fkey\"",
        )
    val ownerId =
        optReference(
            "ownerId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Link_ownerId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Link_pkey\"")

    init {
        index("\"Link_documentId_idx\"", false, documentId)
        index("\"Link_teamId_idx\"", false, teamId)
        index("\"Link_documentId_isArchived_idx\"", false, documentId, isArchived)
        index("\"Link_permissionGroupId_idx\"", false, permissionGroupId)
        index("\"Link_deletedAt_idx\"", false, deletedAt)
        index("\"Link_ownerId_idx\"", false, ownerId)
        index("\"Link_brandId_idx\"", false, brandId)
        uniqueIndex("\"Link_domainSlug_slug_key\"", domainSlug, slug)
    }
}
