use super::display_options::displayOptions;
use super::survey_script_mode::SurveyScriptMode;
use super::survey_status::SurveyStatus;
use super::survey_type::SurveyType;
use super::survey_visibility::SurveyVisibility;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Survey")]
pub struct Model {
    #[sea_orm(column_name = "hiddenFields", default_value = "{\"enabled\": false}")]
    pub hidden_fields: Json,
    #[sea_orm(default_value = "[]")]
    pub variables: Json,
    #[sea_orm(column_name = "ownershipInitialized", default_value = false)]
    pub ownership_initialized: bool,
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub name: String,
    #[sea_orm(column_name = "redirectUrl")]
    pub redirect_url: Option<String>,
    #[sea_orm(default_value = "app")]
    pub r#type: SurveyType,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
    #[sea_orm(column_name = "createdBy")]
    pub created_by: Option<String>,
    #[sea_orm(default_value = "workspace")]
    pub visibility: SurveyVisibility,
    #[sea_orm(column_name = "ownerId")]
    pub owner_id: Option<String>,
    #[sea_orm(column_name = "visibilityVersion", default_value = 0)]
    pub visibility_version: i32,
    #[sea_orm(column_name = "visibilityProjectedVersion", default_value = 0)]
    pub visibility_projected_version: i32,
    #[sea_orm(column_name = "visibilityPending", default_value = false)]
    pub visibility_pending: bool,
    #[sea_orm(column_name = "visibilityChangedAt")]
    pub visibility_changed_at: Option<DateTime>,
    #[sea_orm(column_name = "visibilityChangedById")]
    pub visibility_changed_by_id: Option<String>,
    #[sea_orm(default_value = "draft")]
    pub status: SurveyStatus,
    #[sea_orm(column_name = "welcomeCard", default_value = "{\"enabled\": false}")]
    pub welcome_card: Json,
    #[sea_orm(default_value = "[]")]
    pub questions: Json,
    pub blocks: Vec<Json>,
    pub endings: Vec<Json>,
    #[sea_orm(column_name = "displayOption", default_value = "displayOnce")]
    pub display_option: displayOptions,
    #[sea_orm(column_name = "recontactDays")]
    pub recontact_days: Option<i32>,
    #[sea_orm(column_name = "displayLimit")]
    pub display_limit: Option<i32>,
    #[sea_orm(column_name = "inlineTriggers")]
    pub inline_triggers: Option<Json>,
    #[sea_orm(column_name = "autoClose")]
    pub auto_close: Option<i32>,
    #[sea_orm(column_name = "keepEndingCardOpen", default_value = false)]
    pub keep_ending_card_open: bool,
    #[sea_orm(column_name = "autoComplete")]
    pub auto_complete: Option<i32>,
    #[sea_orm(default_value = 0)]
    pub delay: i32,
    #[sea_orm(column_name = "publishOn")]
    pub publish_on: Option<DateTime>,
    #[sea_orm(column_name = "closeOn")]
    pub close_on: Option<DateTime>,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<DateTime>,
    #[sea_orm(column_name = "surveyClosedMessage")]
    pub survey_closed_message: Option<Json>,
    #[sea_orm(column_name = "segmentId")]
    pub segment_id: Option<String>,
    #[sea_orm(column_name = "workspaceOverwrites")]
    pub workspace_overwrites: Option<Json>,
    pub styling: Option<Json>,
    #[sea_orm(
        column_name = "singleUse",
        default_value = "{\"enabled\": false, \"isEncrypted\": true}"
    )]
    pub single_use: Option<Json>,
    #[sea_orm(column_name = "isVerifyEmailEnabled", default_value = false)]
    pub is_verify_email_enabled: bool,
    #[sea_orm(column_name = "isBackButtonHidden", default_value = false)]
    pub is_back_button_hidden: bool,
    #[sea_orm(column_name = "isAutoProgressingEnabled", default_value = false)]
    pub is_auto_progressing_enabled: bool,
    #[sea_orm(column_name = "isCaptureIpEnabled", default_value = false)]
    pub is_capture_ip_enabled: bool,
    #[sea_orm(default_value = false)]
    pub is_anonymize_responses_enabled: bool,
    pub pin: Option<String>,
    #[sea_orm(column_name = "displayPercentage")]
    pub display_percentage: Option<Decimal>,
    #[sea_orm(column_name = "showLanguageSwitch")]
    pub show_language_switch: Option<bool>,
    #[sea_orm(column_name = "autoSelectLanguage")]
    pub auto_select_language: Option<bool>,
    #[sea_orm(default_value = "{\"enabled\": false, \"threshold\":0.1}")]
    pub recaptcha: Option<Json>,
    #[sea_orm(default_value = "{}")]
    pub metadata: Json,
    #[sea_orm(unique)]
    pub slug: Option<String>,
    #[sea_orm(column_name = "customHeadScripts")]
    pub custom_head_scripts: Option<String>,
    #[sea_orm(column_name = "customHeadScriptsMode", default_value = "add")]
    pub custom_head_scripts_mode: Option<SurveyScriptMode>,
    #[sea_orm(column_name = "customCss")]
    pub custom_css: Option<Json>,
    #[sea_orm(column_name = "showConditions")]
    pub show_conditions: Option<Json>,
    #[sea_orm(column_name = "startLogic")]
    pub start_logic: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workspace::Entity",
        from = "Column::WorkspaceId",
        to = "super::workspace::Column::Id",
        on_delete = "Cascade"
    )]
    Workspace,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedBy",
        to = "super::user::Column::Id"
    )]
    Creator,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::OwnerId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    Owner,
    #[sea_orm(
        belongs_to = "super::segment::Entity",
        from = "Column::SegmentId",
        to = "super::segment::Column::Id"
    )]
    Segment,
    #[sea_orm(has_many = "super::response::Entity")]
    Responses,
    #[sea_orm(has_many = "super::survey_quota::Entity")]
    Quotas,
    #[sea_orm(has_many = "super::survey_trigger::Entity")]
    Triggers,
    #[sea_orm(has_many = "super::survey_attribute_filter::Entity")]
    AttributeFilters,
    #[sea_orm(has_many = "super::display::Entity")]
    Displays,
    #[sea_orm(has_many = "super::survey_language::Entity")]
    Languages,
    #[sea_orm(has_many = "super::embedded_data::Entity")]
    EmbeddedData,
    #[sea_orm(has_many = "super::survey_embedded_data::Entity")]
    EmbeddedDataLinks,
    #[sea_orm(has_many = "super::survey_follow_up::Entity")]
    FollowUps,
    #[sea_orm(has_many = "super::feedback_source_formbricks_mapping::Entity")]
    FeedbackSourceMappings,
    #[sea_orm(has_many = "super::retention_exemption::Entity")]
    RetentionExemptions,
    #[sea_orm(has_many = "super::retention_notice::Entity")]
    RetentionNotices,
    #[sea_orm(has_many = "super::creative::Entity")]
    Creatives,
    #[sea_orm(has_many = "super::attachment_export::Entity")]
    AttachmentExports,
    #[sea_orm(has_one = "super::retention_survey_restore::Entity")]
    RetentionRestore,
}

impl Related<super::workspace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspace.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Creator.def()
    }
}

impl Related<super::segment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Segment.def()
    }
}

impl Related<super::response::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Responses.def()
    }
}

impl Related<super::survey_quota::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Quotas.def()
    }
}

impl Related<super::survey_trigger::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Triggers.def()
    }
}

impl Related<super::survey_attribute_filter::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AttributeFilters.def()
    }
}

impl Related<super::display::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Displays.def()
    }
}

impl Related<super::survey_language::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Languages.def()
    }
}

impl Related<super::embedded_data::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmbeddedData.def()
    }
}

impl Related<super::survey_embedded_data::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmbeddedDataLinks.def()
    }
}

impl Related<super::survey_follow_up::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FollowUps.def()
    }
}

impl Related<super::feedback_source_formbricks_mapping::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FeedbackSourceMappings.def()
    }
}

impl Related<super::retention_exemption::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RetentionExemptions.def()
    }
}

impl Related<super::retention_notice::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RetentionNotices.def()
    }
}

impl Related<super::creative::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Creatives.def()
    }
}

impl Related<super::attachment_export::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AttachmentExports.def()
    }
}

impl Related<super::retention_survey_restore::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RetentionRestore.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
