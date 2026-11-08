package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.JsonBColumnType
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SurveyTable : IdTable<String>("\"Survey\"") {
    val hiddenFields = jsonb<String>("hiddenFields", { it }, { it }).default("{\"enabled\": false}")
    val variables = jsonb<String>("variables", { it }, { it }).default("[]")
    val ownershipInitialized = bool("ownershipInitialized").default(false)
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val redirectUrl = text("redirectUrl").nullable()
    val type = pgEnum("type", "\"SurveyType\"", SurveyType.entries, SurveyType::dbName).default(SurveyType.APP)
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Survey_workspaceId_fkey\"",
        )
    val createdBy =
        optReference(
            "createdBy",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Survey_createdBy_fkey\"",
        )
    val visibility =
        pgEnum("visibility", "\"SurveyVisibility\"", SurveyVisibility.entries, SurveyVisibility::dbName).default(SurveyVisibility.WORKSPACE)
    val ownerId =
        optReference(
            "ownerId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Survey_ownerId_fkey\"",
        )
    val visibilityVersion = integer("visibilityVersion").default(0)
    val visibilityProjectedVersion = integer("visibilityProjectedVersion").default(0)
    val visibilityPending = bool("visibilityPending").default(false)
    val visibilityChangedAt = pgTimestamp("visibilityChangedAt", 3).nullable()
    val visibilityChangedById = text("visibilityChangedById").nullable()
    val status = pgEnum("status", "\"SurveyStatus\"", SurveyStatus.entries, SurveyStatus::dbName).default(SurveyStatus.DRAFT)
    val welcomeCard = jsonb<String>("welcomeCard", { it }, { it }).default("{\"enabled\": false}")
    val questions = jsonb<String>("questions", { it }, { it }).default("[]")
    val blocks =
        pgList("blocks", JsonBColumnType<String>({ it }, { it }), String::class.java).nullable().databaseDefault("'{}'") { emptyList() }
    val endings =
        pgList("endings", JsonBColumnType<String>({ it }, { it }), String::class.java).nullable().databaseDefault("'{}'") { emptyList() }
    val displayOption =
        pgEnum("displayOption", "\"displayOptions\"", DisplayOptions.entries, DisplayOptions::dbName).default(DisplayOptions.DISPLAY_ONCE)
    val recontactDays = integer("recontactDays").nullable()
    val displayLimit = integer("displayLimit").nullable()
    val inlineTriggers = jsonb<String>("inlineTriggers", { it }, { it }).nullable()
    val autoClose = integer("autoClose").nullable()
    val keepEndingCardOpen = bool("keepEndingCardOpen").default(false)
    val autoComplete = integer("autoComplete").nullable()
    val delay = integer("delay").default(0)
    val publishOn = pgTimestamp("publishOn", 3).nullable()
    val closeOn = pgTimestamp("closeOn", 3).nullable()
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val surveyClosedMessage = jsonb<String>("surveyClosedMessage", { it }, { it }).nullable()
    val segmentId =
        optReference(
            "segmentId",
            SegmentTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Survey_segmentId_fkey\"",
        )
    val workspaceOverwrites = jsonb<String>("workspaceOverwrites", { it }, { it }).nullable()
    val styling = jsonb<String>("styling", { it }, { it }).nullable()
    val singleUse = jsonb<String>("singleUse", { it }, { it }).nullable().default("{\"enabled\": false, \"isEncrypted\": true}")
    val isVerifyEmailEnabled = bool("isVerifyEmailEnabled").default(false)
    val isBackButtonHidden = bool("isBackButtonHidden").default(false)
    val isAutoProgressingEnabled = bool("isAutoProgressingEnabled").default(false)
    val isCaptureIpEnabled = bool("isCaptureIpEnabled").default(false)
    val isAnonymizeResponsesEnabled = bool("is_anonymize_responses_enabled").default(false)
    val pin = text("pin").nullable()
    val displayPercentage = decimal("displayPercentage", 65, 30).nullable()
    val showLanguageSwitch = bool("showLanguageSwitch").nullable()
    val autoSelectLanguage = bool("autoSelectLanguage").nullable()
    val recaptcha = jsonb<String>("recaptcha", { it }, { it }).nullable().default("{\"enabled\": false, \"threshold\":0.1}")
    val metadata = jsonb<String>("metadata", { it }, { it }).default("{}")
    val slug = text("slug").nullable().uniqueIndex("\"Survey_slug_key\"")
    val customHeadScripts = text("customHeadScripts").nullable()
    val customHeadScriptsMode =
        pgEnum("customHeadScriptsMode", "\"SurveyScriptMode\"", SurveyScriptMode.entries, SurveyScriptMode::dbName)
            .nullable()
            .default(SurveyScriptMode.ADD)
    val customCss = jsonb<String>("customCss", { it }, { it }).nullable()
    val showConditions = jsonb<String>("showConditions", { it }, { it }).nullable()
    val startLogic = jsonb<String>("startLogic", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Survey_pkey\"")

    init {
        index("\"Survey_workspaceId_updated_at_idx\"", false, workspaceId, updatedAt)
        index("\"Survey_workspaceId_ownerId_idx\"", false, workspaceId, ownerId)
        index("\"Survey_segmentId_idx\"", false, segmentId)
        index("\"Survey_status_publishOn_idx\"", false, status, publishOn)
        index("\"Survey_status_closeOn_idx\"", false, status, closeOn)
        index("\"Survey_archivedAt_idx\"", false, archivedAt)
        uniqueIndex("\"Survey_id_workspaceId_key\"", id, workspaceId)
    }
}
