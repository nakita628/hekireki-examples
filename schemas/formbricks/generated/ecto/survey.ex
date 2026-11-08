defmodule Formbricks.Survey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          hidden_fields: map(),
          variables: map(),
          ownership_initialized: boolean(),
          id: String.t(),
          name: String.t(),
          redirect_url: String.t() | nil,
          type: atom(),
          visibility: atom(),
          visibility_version: integer(),
          visibility_projected_version: integer(),
          visibility_pending: boolean(),
          visibility_changed_at: DateTime.t() | nil,
          visibility_changed_by_id: String.t() | nil,
          status: atom(),
          welcome_card: map(),
          questions: map(),
          blocks: [map()],
          endings: [map()],
          display_option: atom(),
          recontact_days: integer() | nil,
          display_limit: integer() | nil,
          inline_triggers: map() | nil,
          auto_close: integer() | nil,
          keep_ending_card_open: boolean(),
          auto_complete: integer() | nil,
          delay: integer(),
          publish_on: DateTime.t() | nil,
          close_on: DateTime.t() | nil,
          archived_at: DateTime.t() | nil,
          survey_closed_message: map() | nil,
          workspace_overwrites: map() | nil,
          styling: map() | nil,
          single_use: map() | nil,
          is_verify_email_enabled: boolean(),
          is_back_button_hidden: boolean(),
          is_auto_progressing_enabled: boolean(),
          is_capture_ip_enabled: boolean(),
          is_anonymize_responses_enabled: boolean(),
          pin: String.t() | nil,
          display_percentage: Decimal.t() | nil,
          show_language_switch: boolean() | nil,
          auto_select_language: boolean() | nil,
          recaptcha: map() | nil,
          metadata: map(),
          slug: String.t() | nil,
          custom_head_scripts: String.t() | nil,
          custom_head_scripts_mode: atom() | nil,
          custom_css: map() | nil,
          show_conditions: map() | nil,
          start_logic: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          creator: Formbricks.User.t() | nil,
          owner: Formbricks.User.t() | nil,
          segment: Formbricks.Segment.t() | nil,
          retention_restore: Formbricks.RetentionSurveyRestore.t() | nil,
          responses: [Formbricks.Response.t()],
          quotas: [Formbricks.SurveyQuota.t()],
          triggers: [Formbricks.SurveyTrigger.t()],
          attribute_filters: [Formbricks.SurveyAttributeFilter.t()],
          displays: [Formbricks.Display.t()],
          languages: [Formbricks.SurveyLanguage.t()],
          follow_ups: [Formbricks.SurveyFollowUp.t()],
          retention_exemptions: [Formbricks.RetentionExemption.t()],
          retention_notices: [Formbricks.RetentionNotice.t()]
        }

  schema "Survey" do
    field(:hidden_fields, :map, default: %{"enabled" => false}, source: :hiddenFields)
    field(:variables, :map)
    field(:ownership_initialized, :boolean, default: false, source: :ownershipInitialized)
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:redirect_url, :string, source: :redirectUrl)
    field(:type, Ecto.Enum, values: [:link, :app], default: :app)
    field(:visibility, Ecto.Enum, values: [:restricted, :workspace], default: :workspace)
    field(:visibility_version, :integer, default: 0, source: :visibilityVersion)
    field(:visibility_projected_version, :integer, default: 0, source: :visibilityProjectedVersion)
    field(:visibility_pending, :boolean, default: false, source: :visibilityPending)
    field(:visibility_changed_at, Formbricks.PrismaDateTime, source: :visibilityChangedAt)
    field(:visibility_changed_by_id, :string, source: :visibilityChangedById)
    field(:status, Ecto.Enum, values: [:draft, :inProgress, :paused, :completed], default: :draft)
    field(:welcome_card, :map, default: %{"enabled" => false}, source: :welcomeCard)
    field(:questions, :map)
    field(:blocks, {:array, :map})
    field(:endings, {:array, :map})
    field(:display_option, Ecto.Enum, values: [:displayOnce, :displayMultiple, :displaySome, :respondMultiple], default: :displayOnce, source: :displayOption)
    field(:recontact_days, :integer, source: :recontactDays)
    field(:display_limit, :integer, source: :displayLimit)
    field(:inline_triggers, :map, source: :inlineTriggers)
    field(:auto_close, :integer, source: :autoClose)
    field(:keep_ending_card_open, :boolean, default: false, source: :keepEndingCardOpen)
    field(:auto_complete, :integer, source: :autoComplete)
    field(:delay, :integer, default: 0)
    field(:publish_on, Formbricks.PrismaDateTime, source: :publishOn)
    field(:close_on, Formbricks.PrismaDateTime, source: :closeOn)
    field(:archived_at, Formbricks.PrismaDateTime, source: :archivedAt)
    field(:survey_closed_message, :map, source: :surveyClosedMessage)
    field(:workspace_overwrites, :map, source: :workspaceOverwrites)
    field(:styling, :map)
    field(:single_use, :map, default: %{"enabled" => false, "isEncrypted" => true}, source: :singleUse)
    field(:is_verify_email_enabled, :boolean, default: false, source: :isVerifyEmailEnabled)
    field(:is_back_button_hidden, :boolean, default: false, source: :isBackButtonHidden)
    field(:is_auto_progressing_enabled, :boolean, default: false, source: :isAutoProgressingEnabled)
    field(:is_capture_ip_enabled, :boolean, default: false, source: :isCaptureIpEnabled)
    field(:is_anonymize_responses_enabled, :boolean, default: false)
    field(:pin, :string)
    field(:display_percentage, :decimal, source: :displayPercentage)
    field(:show_language_switch, :boolean, source: :showLanguageSwitch)
    field(:auto_select_language, :boolean, source: :autoSelectLanguage)
    field(:recaptcha, :map, default: %{"enabled" => false, "threshold" => 0.1})
    field(:metadata, :map, default: %{})
    field(:slug, :string)
    field(:custom_head_scripts, :string, source: :customHeadScripts)
    field(:custom_head_scripts_mode, Ecto.Enum, values: [:add, :replace], default: :add, source: :customHeadScriptsMode)
    field(:custom_css, :map, source: :customCss)
    field(:show_conditions, :map, source: :showConditions)
    field(:start_logic, :map, source: :startLogic)
    field(:workspace_id, :string, source: :workspaceId)
    field(:created_by, :string, source: :createdBy)
    field(:owner_id, :string, source: :ownerId)
    field(:segment_id, :string, source: :segmentId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:creator, Formbricks.User, foreign_key: :created_by, define_field: false, type: :string)
    belongs_to(:owner, Formbricks.User, foreign_key: :owner_id, define_field: false, type: :string)
    belongs_to(:segment, Formbricks.Segment, foreign_key: :segment_id, define_field: false, type: :string)
    has_one(:retention_restore, Formbricks.RetentionSurveyRestore, foreign_key: :survey_id, references: :id)
    has_many(:responses, Formbricks.Response, foreign_key: :survey_id, references: :id)
    has_many(:quotas, Formbricks.SurveyQuota, foreign_key: :survey_id, references: :id)
    has_many(:triggers, Formbricks.SurveyTrigger, foreign_key: :survey_id, references: :id)
    has_many(:attribute_filters, Formbricks.SurveyAttributeFilter, foreign_key: :survey_id, references: :id)
    has_many(:displays, Formbricks.Display, foreign_key: :survey_id, references: :id)
    has_many(:languages, Formbricks.SurveyLanguage, foreign_key: :survey_id, references: :id)
    has_many(:follow_ups, Formbricks.SurveyFollowUp, foreign_key: :survey_id, references: :id)
    has_many(:retention_exemptions, Formbricks.RetentionExemption, foreign_key: :survey_id, references: :id)
    has_many(:retention_notices, Formbricks.RetentionNotice, foreign_key: :survey_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end